// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.Application.Abstractions.Providers;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.IoC.Options;
using MyFinances.IoC.Persistence.Repositories;
using MyFinances.IoC.Providers;

namespace MyFinances.IoC;

public static class ServiceCollectionExtensions {
  public static IServiceCollection AddIoCLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .AddIoCOptions(configuration)
      .AddRepositories()
      .AddProviders();

  private static IServiceCollection AddIoCOptions(
    this IServiceCollection serviceCollection, IConfiguration configuration
  ) => serviceCollection
    .Configure<JwtTokenOptions>(configuration.GetSection("JwtToken"));

  private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddSingleton<IUserRepository, UserRepository>(); // Singleton until we have a database

  private static IServiceCollection AddProviders(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddSingleton<IDateTimeProvider, DateTimeProvider>()
      .AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
}