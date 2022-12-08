// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;
using MyFinances.IoC.Persistence.Primitives;

namespace MyFinances.IoC.Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository {
  private readonly ILogger<UserRepository> _logger;

  public UserRepository(ILogger<UserRepository> logger) : base(logger, new List<User>())
    => _logger = logger;

  public async Task<User?> FindByEmailAsync(string email) {
    _logger.LogInformation("Finding user by email: {Email}", email);

    return await Task.FromResult(Entities.FirstOrDefault(u => u.Email == email));
  }

  public async Task<User?> FindByUserNameAsync(string userName) {
    _logger.LogInformation("Finding user by username: {UserName}", userName);

    return await Task.FromResult(Entities.FirstOrDefault(u => u.Username == userName));
  }
}

