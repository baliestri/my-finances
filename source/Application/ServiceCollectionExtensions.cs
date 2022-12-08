// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.Application.Behaviors;

namespace MyFinances.Application;

public static class ServiceCollectionExtensions {
  public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
      .AddMediatRBehaviors()
      .AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

  private static IServiceCollection AddMediatRBehaviors(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
}