// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Application;
using MyFinances.IoC;

namespace MyFinances.WebAPI.Extensions;

/// <summary>
///   Extension methods for <see cref="WebApplicationBuilder" />.
/// </summary>
public static class WebApplicationBuilderExtensions {
  /// <summary>
  ///   Register the required components for the application.
  /// </summary>
  /// <param name="builder">The <see cref="WebApplicationBuilder" /> instance.</param>
  /// <returns></returns>
  public static WebApplication RegisterComponents(this WebApplicationBuilder builder) {
    var configuration = builder.Configuration;
    var serviceCollection = builder.Services;

    serviceCollection
      .AddLogger()
      .AddSwagger()
      .AddConfiguredRouting()
      .AddCustomProblemDetailsFactory()
      .AddApplicationLayer()
      .AddIoCLayer(configuration)
      .AddMappings()
      .AddControllers();

    return builder.Build();
  }
}
