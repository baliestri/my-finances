// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MyFinances.WebAPI.Extensions;

/// <summary>
///   Extension methods for <see cref="WebApplication" />.
/// </summary>
public static class WebApplicationExtensions {
  /// <summary>
  ///   Register the required pipelines for the application.
  /// </summary>
  /// <param name="app">The <see cref="WebApplication" /> instance.</param>
  /// <returns></returns>
  public static WebApplication RegisterPipelines(this WebApplication app) {
    var configuration = app.Configuration;
    var serviceProvider = app.Services;

    app
      .UseSwaggerForDevelopment()
      .UseExceptionHandler("/api/error")
      .UseHttpsRedirection()
      .UseRouting()
      .UseAuthentication()
      .UseAuthorization()
      .UseEndpoints(builder => builder.MapControllers());

    return app;
  }

  private static WebApplication UseSwaggerForDevelopment(this WebApplication app) {
    var isDevelopment = app.Environment.IsDevelopment();

    if (isDevelopment) {
      app.UseSwagger();

      using var scope = app.Services.CreateScope();
      var provider = scope.ServiceProvider.GetService<IApiVersionDescriptionProvider>();

      app.UseSwaggerUI(
        options => provider?.ApiVersionDescriptions.ToList().ForEach(
          description => options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant()
          )
        )
      );
    }

    return app;
  }
}