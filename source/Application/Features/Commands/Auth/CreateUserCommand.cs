// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using MediatR;
using MyFinances.Application.Contracts.Responses;

namespace MyFinances.Application.Features.Commands.Auth;

public sealed record CreateUserCommand(
  string UserName,
  string FirstName,
  string LastName,
  string Email,
  string Password
) : IRequest<ErrorOr<SuccessfulAuthResponse>>;