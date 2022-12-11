// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyFinances.Application.Abstractions.Providers;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.IoC.Options;
using MyFinances.IoC.Persistence;
using MyFinances.IoC.Persistence.Repositories;
using MyFinances.IoC.Providers;
using MSOptions = Microsoft.Extensions.Options.Options;

namespace MyFinances.IoC;

public static class ServiceCollectionExtensions {
  public static IServiceCollection AddIoCLayer(
    this IServiceCollection serviceCollection, IConfiguration configuration
  ) {
    var jwtTokenOptions = new JwtTokenOptions();
    configuration.Bind("MyFinances:JwtToken", jwtTokenOptions);

    serviceCollection
      .AddSingleton(MSOptions.Create(jwtTokenOptions))
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        options
          => options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtTokenOptions.Issuer,
            ValidAudience = jwtTokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Secret))
          }
      );
    serviceCollection
      .AddRepositories()
      .AddProviders()
      .AddDatabase(configuration);

    return serviceCollection;
  }

  private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddScoped<IUserRepository, UserRepository>();

  private static IServiceCollection AddProviders(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddSingleton<IDateTimeProvider, DateTimeProvider>()
      .AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

  private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .AddDbContext<DataContext>(
        options
          => options.UseSqlServer(configuration["MyFinances:Database:ConnectionString"])
      );
}