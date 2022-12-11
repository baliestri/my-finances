// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace MyFinances.Application.Features.Commands.Auth;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
  public CreateUserCommandValidator() {
    RuleFor(command => command.UserName)
      .NotEmpty()
      .MinimumLength(5);
    RuleFor(command => command.FirstName)
      .NotEmpty();
    RuleFor(command => command.LastName)
      .NotEmpty();
    RuleFor(command => command.Email)
      .NotEmpty()
      .EmailAddress();
    RuleFor(command => command.Password)
      .NotEmpty()
      .MinimumLength(12)
      .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{12,}$");
  }
}