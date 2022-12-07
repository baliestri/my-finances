// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;

namespace MyFinances.Application.Errors;

public static class AuthErrors {
  public static Error InvalidEmail
    => Error.Custom(
      (int)HttpErrorType.BadRequest,
      "ERR_AUTH_INVALID_EMAIL",
      "The provided email is invalid."
    );

  public static Error InvalidPassword
    => Error.Custom(
      (int)HttpErrorType.BadRequest,
      "ERR_AUTH_INVALID_PASSWORD",
      "The provided password is invalid."
    );

  public static Error InvalidCredentials
    => Error.Custom(
      (int)HttpErrorType.Unauthorized,
      "ERR_AUTH_INVALID_CREDENTIALS",
      "The provided credentials does not match any user."
    );

  public static Error InvalidAccessToken
    => Error.Custom(
      (int)HttpErrorType.Unauthorized,
      "ERR_AUTH_INVALID_ACCESS_TOKEN",
      "The provided access token is invalid."
    );

  public static Error DuplicateEmail
    => Error.Custom(
      (int)HttpErrorType.Conflict,
      "ERR_AUTH_DUPLICATE_EMAIL",
      "The provided email is already in use."
    );

  public static Error DuplicateUsername
    => Error.Custom(
      (int)HttpErrorType.Conflict,
      "ERR_AUTH_DUPLICATE_USERNAME",
      "The provided username is already in use."
    );
}
