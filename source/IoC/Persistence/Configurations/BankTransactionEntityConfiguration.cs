// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinances.Core.Entities;

namespace MyFinances.IoC.Persistence.Configurations;

public sealed class BankTransactionEntityConfiguration : IEntityTypeConfiguration<BankTransaction> {
  public void Configure(EntityTypeBuilder<BankTransaction> builder) {
    builder.HasKey(transaction => transaction.Id);
    builder
      .Property(transaction => transaction.Title)
      .HasMaxLength(50)
      .IsRequired();
    builder
      .Property(transaction => transaction.Description)
      .HasMaxLength(200);
    builder
      .Property(transaction => transaction.Amount)
      .HasPrecision(10, 2)
      .IsRequired();
    builder
      .Property(transaction => transaction.Date)
      .IsRequired();
    builder
      .Property(transaction => transaction.Type)
      .IsRequired();

    builder
      .HasOne(transaction => transaction.BankAccount)
      .WithMany(account => account.Transactions)
      .HasConstraintName("FK_BankTransaction_BankAccount");
  }
}