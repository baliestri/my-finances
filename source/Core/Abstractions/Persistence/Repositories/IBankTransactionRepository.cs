// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Abstractions.Persistence.Primitives;
using MyFinances.Core.Entities;

namespace MyFinances.Core.Abstractions.Persistence.Repositories;

public interface IBankTransactionRepository : IRepository<BankTransaction> {
  Task<IEnumerable<BankTransaction>> GetByBankAccountIdAsync(Guid bankAccountId);
  Task<IEnumerable<BankTransaction>> FilterByDateAsync(DateTime date);
  Task<IEnumerable<BankTransaction>> FilterByDateRangeAsync(DateTime startDate, DateTime endDate);
  Task<IEnumerable<BankTransaction>> FilterByAmountRangeAsync(decimal minAmount, decimal maxAmount);
}