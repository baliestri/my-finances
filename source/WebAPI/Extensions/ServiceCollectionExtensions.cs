// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using MyFinances.WebAPI.Factories;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace MyFinances.WebAPI.Extensions;

/// <summary>
///   Extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions {
  private static readonly string _xmlFile = Path.Combine(
    AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
  );

  private static readonly string _loggerFile = Path.Join(
    AppContext.BaseDirectory, "logs", "log-.txt"
  );

  private static readonly ExpressionTemplate _loggerTemplate = new(
    "[{@t:HH:mm:ss} {@l:u3} ({Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)})] {@m}\n{@x}",
    theme: TemplateTheme.Literate
  );

  /// <summary>
  ///   Adds configured services required for routing requests.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <returns></returns>
  public static IServiceCollection AddConfiguredRouting(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddRouting(options => options.LowercaseUrls = true);

  /// <summary>
  ///   Adds the logger to the service collection.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <returns></returns>
  public static IServiceCollection AddLogger(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddLogging(
        builder => builder
          .ClearProviders()
          .AddSerilog(
            new LoggerConfiguration()
              .WriteTo.File(_loggerFile, rollingInterval: RollingInterval.Day)
              .WriteTo.Console(_loggerTemplate)
              .CreateLogger()
          )
      );

  /// <summary>
  ///   Adds the Swagger to the service collection.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <returns></returns>
  public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddApiVersioning(
        options => {
          options.DefaultApiVersion = new ApiVersion(1, 0);
          options.AssumeDefaultVersionWhenUnspecified = true;
          options.ReportApiVersions = true;
        }
      )
      .AddVersionedApiExplorer(
        options => {
          options.GroupNameFormat = "'v'VVV";
          options.SubstituteApiVersionInUrl = true;
        }
      )
      .AddSwaggerGen(
        options => {
          options.SwaggerDoc(
            "v1", new OpenApiInfo {
              Title = "MyFinances", Version = "v1",
              License = new OpenApiLicense {
                Name = "MIT",
                Url = new Uri("https://github.com/baliestri/my-finances/blob/main/LICENSE")
              }
            }
          );
          options.IncludeXmlComments(_xmlFile);

          var securityScheme = new OpenApiSecurityScheme {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
          };

          var securityRequirement = new OpenApiSecurityRequirement {
            { securityScheme, new[] { "Bearer" } }
          };

          options.AddSecurityDefinition("Bearer", securityScheme);
          options.AddSecurityRequirement(securityRequirement);
        }
      );

  /// <summary>
  ///   Adds the custom problem details factory to the service collection.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <returns></returns>
  public static IServiceCollection AddCustomProblemDetailsFactory(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();

  /// <summary>
  ///   Adds the Mapster to the service collection.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <returns></returns>
  public static IServiceCollection AddMappings(this IServiceCollection serviceCollection) {
    var config = TypeAdapterConfig.GlobalSettings;
    config.Scan(typeof(IServiceCollection).Assembly);

    return serviceCollection
      .AddSingleton(config)
      .AddScoped<IMapper, ServiceMapper>();
  }
}