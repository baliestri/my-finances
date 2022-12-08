// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using MyFinances.Application.Abstractions.Providers;
using MyFinances.Application.Contracts.Responses;
using MyFinances.Application.Errors;
using MyFinances.Core.Abstractions.Persistence.Repositories;

namespace MyFinances.Application.Features.Queries.Auth;

public sealed class SignInUserQueryHandler : IRequestHandler<SignInUserQuery, ErrorOr<SuccessfulAuthResponse>> {
  private readonly ILogger<SignInUserQueryHandler> _logger;
  private readonly IJwtTokenProvider _tokenProvider;
  private readonly IUserRepository _userRepository;

  public SignInUserQueryHandler(
    ILogger<SignInUserQueryHandler> logger, IJwtTokenProvider tokenProvider, IUserRepository userRepository
  ) {
    _logger = logger;
    _tokenProvider = tokenProvider;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<SuccessfulAuthResponse>> Handle(
    SignInUserQuery request, CancellationToken cancellationToken
  ) {
    var (userName, email, password) = request;
    _logger.LogInformation("Attempting to sign in user {UserName} ({Email})", userName, email);

    var isEmailEmpty = string.IsNullOrWhiteSpace(email);

    var user = isEmailEmpty
      ? await _userRepository.FindByUserNameAsync(userName!)
      : await _userRepository.FindByEmailAsync(email!);

    if (user is null) {
      _logger.LogWarning("The provided username or email was not found");
      return AuthErrors.InvalidCredentials;
    }

    if (!user.Password.Equals(password)) {
      _logger.LogWarning("The provided password does not match");
      return AuthErrors.InvalidCredentials;
    }

    var accessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Username, user.Email);
    var accessTokenExpirationDate = _tokenProvider.GetExpirationDate(accessToken);
    _logger.LogInformation("User signed in successfully");

    return new SuccessfulAuthResponse(accessToken, accessTokenExpirationDate);
  }
}
