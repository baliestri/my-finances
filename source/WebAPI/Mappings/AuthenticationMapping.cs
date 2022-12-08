// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Mapster;
using MyFinances.Application.Contracts.Requests;
using MyFinances.Application.Features.Commands.Auth;
using MyFinances.Application.Features.Queries.Auth;

namespace MyFinances.WebAPI.Mappings;

/// <summary>
///   Maps the request objects to the command/query objects for the authentication feature.
/// </summary>
public sealed class AuthenticationMapping : IRegister {
  /// <summary>
  ///   Registers the mappings.
  /// </summary>
  /// <param name="config">The configuration.</param>
  public void Register(TypeAdapterConfig config) {
    config.NewConfig<CreateUserRequest, CreateUserCommand>();
    config.NewConfig<SignInUserRequest, SignInUserQuery>();
  }
}