// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Entities;

public sealed class BankTransaction : BaseEntity {
  public BankTransaction() { }

  public BankTransaction(
    string title, string? description, decimal amount, DateTime date, BankAccount bankAccount
  ) {
    Title = title;
    Description = description;
    Amount = amount;
    Date = date;
    BankAccount = bankAccount;
  }

  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public DateTime Date { get; set; }

  public BankAccount BankAccount { get; set; } = null!;
}