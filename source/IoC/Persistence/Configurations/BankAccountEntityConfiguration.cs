// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinances.Core.Entities;

namespace MyFinances.IoC.Persistence.Configurations;

public sealed class BankAccountEntityConfiguration : IEntityTypeConfiguration<BankAccount> {
  public void Configure(EntityTypeBuilder<BankAccount> builder) {
    builder.HasKey(account => account.Id);
    builder
      .Property(account => account.Agency)
      .HasMaxLength(10)
      .IsRequired();
    builder
      .Property(account => account.Account)
      .HasMaxLength(20)
      .IsRequired();
    builder
      .Property(account => account.Balance)
      .HasPrecision(10, 2)
      .IsRequired();

    builder
      .HasOne(account => account.Bank)
      .WithMany(bank => bank.Accounts)
      .HasConstraintName("FK_BankAccount_Bank");

    builder
      .HasOne(account => account.User)
      .WithMany(user => user.BankAccounts)
      .HasConstraintName("FK_BankAccount_User");

    builder
      .HasMany(account => account.Transactions)
      .WithOne(transaction => transaction.BankAccount)
      .HasConstraintName("FK_BankTransaction_BankAccount");
  }
}
