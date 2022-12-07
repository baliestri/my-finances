// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace MyFinances.WebAPI.Factories;

/// <summary>
///   A factory for creating <see cref="ProblemDetails" /> and <see cref="ValidationProblemDetails" /> instances.
/// </summary>
public sealed class CustomProblemDetailsFactory : ProblemDetailsFactory {
  private readonly ApiBehaviorOptions _behaviorOptions;
  private readonly Action<ProblemDetailsContext>? _configure;

  /// <summary>
  ///   Initializes a new instance of the <see cref="CustomProblemDetailsFactory" /> class.
  /// </summary>
  /// <param name="behaviorOptions">The options for the API behavior.</param>
  /// <param name="problemDetailsOptions">The options for the problem details.</param>
  public CustomProblemDetailsFactory(
    IOptions<ApiBehaviorOptions> behaviorOptions,
    IOptions<ProblemDetailsOptions>? problemDetailsOptions = null
  ) {
    _behaviorOptions = behaviorOptions.Value;
    _configure = problemDetailsOptions?.Value.CustomizeProblemDetails;
  }

  /// <inheritdoc />
  public override ProblemDetails CreateProblemDetails(
    HttpContext httpContext,
    int? statusCode = null,
    string? title = null,
    string? type = null,
    string? detail = null,
    string? instance = null
  ) {
    statusCode ??= 500;

    var problemDetails = new ProblemDetails {
      Status = statusCode,
      Title = title,
      Type = type,
      Detail = detail,
      Instance = instance
    };

    ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

    return problemDetails;
  }

  /// <inheritdoc />
  public override ValidationProblemDetails CreateValidationProblemDetails(
    HttpContext httpContext,
    ModelStateDictionary modelStateDictionary,
    int? statusCode = null,
    string? title = null,
    string? type = null,
    string? detail = null,
    string? instance = null
  ) {
    if (modelStateDictionary == null) {
      throw new ArgumentNullException(nameof(modelStateDictionary));
    }

    statusCode ??= 400;

    var problemDetails = new ValidationProblemDetails(modelStateDictionary) {
      Status = statusCode,
      Type = type,
      Detail = detail,
      Instance = instance
    };

    if (title != null) {
      problemDetails.Title = title;
    }

    ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

    return problemDetails;
  }

  private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode) {
    problemDetails.Status ??= statusCode;

    if (_behaviorOptions.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData)) {
      problemDetails.Title ??= clientErrorData.Title;
      problemDetails.Type ??= clientErrorData.Link;
    }

    var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
    if (traceId != null) {
      problemDetails.Extensions["traceId"] = traceId;
    }

    _configure?.Invoke(
      new ProblemDetailsContext {
        HttpContext = httpContext!,
        ProblemDetails = problemDetails
      }
    );
  }
}
