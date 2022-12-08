// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace MyFinances.Application.Contracts.Requests;

public sealed record CreateUserRequest(
  string UserName,
  string FirstName,
  string LastName,
  string Email,
  string Password
);