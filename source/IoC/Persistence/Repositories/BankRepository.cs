// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;
using MyFinances.IoC.Persistence.Primitives;

namespace MyFinances.IoC.Persistence.Repositories;

public sealed class BankRepository : Repository<Bank>, IBankRepository {
  private readonly DataContext _context;
  private readonly ILogger<BankRepository> _logger;

  public BankRepository(ILogger<BankRepository> logger, DataContext context) : base(logger, context) {
    _logger = logger;
    _context = context;
  }

  public async Task<Bank?> GetByNameAsync(string name) {
    _logger.LogInformation("Getting bank by name: {Name}", name);

    return await _context
      .Banks
      .FirstOrDefaultAsync(bank => bank.Name == name);
  }
}