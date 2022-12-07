// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyFinances.WebAPI.Controllers;

/// <summary>
///   Controller for handling errors.
/// </summary>
public sealed class ErrorController : ControllerBase {
  /// <summary>
  ///   Handles errors.
  /// </summary>
  /// <returns>The error response.</returns>
  [Route("api/error")]
  public IActionResult Error() {
    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
    var exception = context?.Error;

    return Problem(title: exception?.Message);
  }
}
