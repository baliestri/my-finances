// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Application.Errors;

namespace MyFinances.WebAPI.Primitives;

/// <summary>
///   A base class for all controllers.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase {
  /// <summary>
  ///   Creates an <see cref="ObjectResult" /> that produces a <see cref="ProblemDetails" /> response.
  /// </summary>
  /// <param name="errors">The errors to be returned.</param>
  /// <returns>The created <see cref="ObjectResult" /> for the response.</returns>
  protected IActionResult Problem(IEnumerable<Error> errors) {
    var error = errors.FirstOrDefault();

    var statusCode = error.Type switch {
      (ErrorType)HttpErrorType.BadRequest => StatusCodes.Status400BadRequest,
      (ErrorType)HttpErrorType.Conflict => StatusCodes.Status409Conflict,
      (ErrorType)HttpErrorType.Forbidden => StatusCodes.Status403Forbidden,
      (ErrorType)HttpErrorType.NotFound => StatusCodes.Status404NotFound,
      (ErrorType)HttpErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      (ErrorType)HttpErrorType.MethodNotAllowed => StatusCodes.Status405MethodNotAllowed,
      var _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: error.Code, detail: error.Description);
  }
}
