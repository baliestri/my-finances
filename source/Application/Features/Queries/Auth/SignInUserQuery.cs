// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using MediatR;
using MyFinances.Application.Contracts.Responses;

namespace MyFinances.Application.Features.Queries.Auth;

public sealed record SignInUserQuery(
  string? UserName,
  string? Email,
  string Password
) : IRequest<ErrorOr<SuccessfulAuthResponse>>;
