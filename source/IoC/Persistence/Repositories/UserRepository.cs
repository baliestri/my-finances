// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;
using MyFinances.IoC.Persistence.Primitives;

namespace MyFinances.IoC.Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository {
  private readonly DataContext _context;
  private readonly ILogger<UserRepository> _logger;

  public UserRepository(ILogger<UserRepository> logger, DataContext context) : base(logger, context) {
    _logger = logger;
    _context = context;
  }

  public async Task<User?> FindByEmailAsync(string email) {
    _logger.LogInformation("Finding user by email: {Email}", email);

    return await _context
      .Users
      .FirstOrDefaultAsync(user => user.Email == email);
  }

  public async Task<User?> FindByUsernameAsync(string username) {
    _logger.LogInformation("Finding user by username: {Username}", username);

    return await _context
      .Users
      .FirstOrDefaultAsync(user => user.Username == username);
  }
}