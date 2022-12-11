// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;
using MyFinances.IoC.Persistence.Primitives;

namespace MyFinances.IoC.Persistence.Repositories;

public sealed class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository {
  private readonly DataContext _context;
  private readonly ILogger<BankAccountRepository> _logger;

  public BankAccountRepository(ILogger<BankAccountRepository> logger, DataContext context) : base(logger, context) {
    _logger = logger;
    _context = context;
  }

  public async Task<BankAccount?> GetByUserIdAsync(Guid userId) {
    _logger.LogInformation("Getting bank account by user id: {UserId}", userId);

    return await _context
      .Accounts
      .FirstOrDefaultAsync(account => account.User.Id == userId);
  }
}