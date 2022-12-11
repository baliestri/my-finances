// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Repositories;
using MyFinances.Core.Entities;
using MyFinances.IoC.Persistence.Primitives;

namespace MyFinances.IoC.Persistence.Repositories;

public sealed class BankTransactionRepository : Repository<BankTransaction>, IBankTransactionRepository {
  private readonly DataContext _context;
  private readonly ILogger<BankTransactionRepository> _logger;

  public BankTransactionRepository(ILogger<BankTransactionRepository> logger, DataContext context)
    : base(logger, context) {
    _context = context;
    _logger = logger;
  }

  public async Task<IEnumerable<BankTransaction>> GetByBankAccountIdAsync(Guid bankAccountId) {
    _logger.LogInformation("Getting all bank transactions by bank account id {BankAccountId}", bankAccountId);

    return await _context
      .Transactions
      .Where(transaction => transaction.BankAccount.Id == bankAccountId)
      .ToListAsync();
  }

  public async Task<IEnumerable<BankTransaction>> FilterByDateAsync(DateTime date) {
    _logger.LogInformation("Filtering bank transactions by date {Date}", date);

    return await _context
      .Transactions
      .Where(transaction => transaction.Date == date)
      .ToListAsync();
  }

  public async Task<IEnumerable<BankTransaction>> FilterByDateRangeAsync(DateTime startDate, DateTime endDate) {
    _logger.LogInformation("Filtering bank transactions by date range {StartDate} - {EndDate}", startDate, endDate);

    return await _context
      .Transactions
      .Where(transaction => transaction.Date >= startDate && transaction.Date <= endDate)
      .ToListAsync();
  }

  public async Task<IEnumerable<BankTransaction>> FilterByAmountRangeAsync(decimal minAmount, decimal maxAmount) {
    _logger.LogInformation(
      "Filtering bank transactions by amount range {MinAmount} - {MaxAmount}", minAmount, maxAmount
    );

    return await _context
      .Transactions
      .Where(transaction => transaction.Amount >= minAmount && transaction.Amount <= maxAmount)
      .ToListAsync();
  }
}