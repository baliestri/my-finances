// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.IoC.Options;

namespace MyFinances.IoC;

public static class ServiceCollectionExtensions {
  public static IServiceCollection AddIoCLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .AddIoCOptions(configuration);

  private static IServiceCollection AddIoCOptions(
    this IServiceCollection serviceCollection, IConfiguration configuration
  ) => serviceCollection
    .Configure<JwtTokenOptions>(configuration.GetSection("JwtToken"));
}
