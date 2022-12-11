// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyFinances.Application.Errors;

namespace MyFinances.WebAPI.Primitives;

/// <summary>
///   A base class for all controllers.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase {
  /// <summary>
  ///   Handles the <see cref="ErrorOr{T}" /> result of an operation.
  /// </summary>
  /// <param name="errors">The error list.</param>
  /// <returns>The compatible <see cref="IActionResult" />.</returns>
  protected IActionResult HandleProblem(IList<Error> errors) {
    if (errors.All(error => error.Type is ErrorType.Validation)) {
      return ValidationProblem(errors);
    }

    HttpContext.Items["errors"] = errors;

    return Problem(errors.FirstOrDefault());
  }

  /// <summary>
  ///   Creates an <see cref="ObjectResult" /> that produces a <see cref="ProblemDetails" /> response.
  /// </summary>
  /// <param name="error">The error to be handled.</param>
  /// <returns>The created <see cref="ObjectResult" /> for the response.</returns>
  private IActionResult Problem(Error error) {
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

  /// <summary>
  ///   Creates an <see cref="ObjectResult" /> that produces a <see cref="ValidationProblemDetails" /> response.
  /// </summary>
  /// <param name="errors">The errors to be handled.</param>
  /// <returns>The created <see cref="ObjectResult" /> for the response.</returns>
  private IActionResult ValidationProblem(IEnumerable<Error> errors) {
    var modelStateDictionary = new ModelStateDictionary();

    foreach (var error in errors) {
      modelStateDictionary.AddModelError(error.Code, error.Description);
    }

    return ValidationProblem(modelStateDictionary);
  }
}