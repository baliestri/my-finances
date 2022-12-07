// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using MyFinances.Application.Abstractions.Providers;
using MyFinances.Application.Contracts.Responses;
using MyFinances.Application.Errors;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;

namespace MyFinances.Application.Features.Commands.Auth;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<SuccessfulAuthResponse>> {
  private readonly ILogger<CreateUserCommandHandler> _logger;
  private readonly IJwtTokenProvider _tokenProvider;
  private readonly IUserRepository _userRepository;

  public CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger, IJwtTokenProvider tokenProvider, IUserRepository userRepository
  ) {
    _logger = logger;
    _tokenProvider = tokenProvider;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<SuccessfulAuthResponse>> Handle(
    CreateUserCommand request, CancellationToken cancellationToken
  ) {
    var (userName, firstName, lastName, email, password) = request;
    _logger.LogInformation("Attempting to create user {UserName} ({Email})", userName, email);

    if (await _userRepository.FindByEmailAsync(email) is not null) {
      _logger.LogWarning("The provided email '{Email}' is already in use", email);
      return AuthErrors.DuplicateEmail;
    }

    if (await _userRepository.FindByUserNameAsync(userName) is not null) {
      _logger.LogWarning("The provided username '{UserName}' is already in use", userName);
      return AuthErrors.DuplicateUsername;
    }

    try {
      var user = new UserEntity(userName, firstName, lastName, email, password);
      await _userRepository.AddAsync(user);
      var accessToken = _tokenProvider.GenerateAccessToken(user.Id, userName, email);
      var accessTokenExpirationDate = _tokenProvider.GetExpirationDate(accessToken);
      _logger.LogInformation("User created successfully");

      return new SuccessfulAuthResponse(accessToken, accessTokenExpirationDate);
    }
    catch (Exception e) {
      _logger.LogError(e, "An error occurred while creating the user");
      return Error.Custom((int)HttpErrorType.Unexpected, "ERR_UNEXPECTED", e.Message);
    }
  }
}
