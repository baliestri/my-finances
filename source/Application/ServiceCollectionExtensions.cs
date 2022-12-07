// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MyFinances.Application;

public static class ServiceCollectionExtensions {
  public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    => serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
}
