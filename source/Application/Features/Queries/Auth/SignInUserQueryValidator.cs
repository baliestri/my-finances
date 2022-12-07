// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace MyFinances.Application.Features.Queries.Auth;

public sealed class SignInUserQueryValidator : AbstractValidator<SignInUserQuery> {
  public SignInUserQueryValidator() {
    RuleFor(query => query.UserName)
      .Null()
      .When(query => query.Email is not null);
    RuleFor(query => query.Email)
      .Null()
      .When(query => query.UserName is not null);
    RuleFor(query => query.Password)
      .NotEmpty()
      .MinimumLength(12)
      .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{12,}$");
  }
}
