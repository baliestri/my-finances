// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Application.Contracts.Requests;
using MyFinances.Application.Contracts.Responses;
using MyFinances.Application.Features.Commands.Auth;
using MyFinances.Application.Features.Queries.Auth;
using MyFinances.WebAPI.Primitives;

namespace MyFinances.WebAPI.Controllers;

/// <summary>
///   A controller for handling user creation and authentication.
/// </summary>
public sealed class AuthController : BaseController {
  private readonly ILogger<AuthController> _logger;
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;

  /// <summary>
  ///   Initializes a new instance of the <see cref="AuthController" /> class.
  /// </summary>
  /// <param name="logger">The logger.</param>
  /// <param name="mediator">The mediator.</param>
  /// <param name="mapper">The mapper.</param>
  public AuthController(ILogger<AuthController> logger, IMediator mediator, IMapper mapper) {
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
  }

  /// <summary>
  ///   Creates a new user.
  /// </summary>
  /// <param name="request">The <see cref="CreateUserRequest" /> containing the user information.</param>
  /// <returns>The <see cref="IActionResult" /> containing the result of the operation.</returns>
  [HttpPost("create")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(SuccessfulAuthResponse), StatusCodes.Status201Created)]
  public async Task<IActionResult> Create(CreateUserRequest request) {
    var (userName, _, _, email, _) = request;
    _logger.LogInformation("Attempting to create user {Username} ({Email})", userName, email);

    var command = _mapper.Map<CreateUserCommand>(request);
    var resultOrError = await _mediator.Send(command);

    return resultOrError.Match(auth => Created("auth", auth), HandleProblem);
  }

  /// <summary>
  ///   Authenticates a user.
  /// </summary>
  /// <param name="request">The <see cref="SignInUserRequest" /> containing the user credentials.</param>
  /// <returns>The <see cref="IActionResult" /> containing the result of the operation.</returns>
  [HttpPost("signin")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(SuccessfulAuthResponse), StatusCodes.Status200OK)]
  public async Task<IActionResult> SignIn(SignInUserRequest request) {
    var (userName, email, _) = request;
    _logger.LogInformation("Attempting to sign in user {Username} ({Email})", userName, email);

    var query = _mapper.Map<SignInUserQuery>(request);
    var resultOrError = await _mediator.Send(query);

    return resultOrError.Match(Ok, HandleProblem);
  }
}
