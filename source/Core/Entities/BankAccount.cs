// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Entities;

public sealed class BankAccount : BaseEntity {
  public string? Agency { get; set; }
  public string? Account { get; set; }
  public decimal Balance { get; set; }

  public Bank Bank { get; set; } = null!;
  public User User { get; set; } = null!;

  public IEnumerable<BankTransaction> Transactions { get; set; } = null!;
}