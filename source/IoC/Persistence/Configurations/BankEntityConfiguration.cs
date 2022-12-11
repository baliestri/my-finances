// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinances.Core.Entities;

namespace MyFinances.IoC.Persistence.Configurations;

public sealed class BankEntityConfiguration : IEntityTypeConfiguration<Bank> {
  public void Configure(EntityTypeBuilder<Bank> builder) {
    builder.HasKey(bank => bank.Id);
    builder
      .Property(bank => bank.Name)
      .HasMaxLength(50)
      .IsRequired();

    builder
      .HasMany(bank => bank.Accounts)
      .WithOne(account => account.Bank)
      .HasConstraintName("FK_Bank_Accounts")
      .OnDelete(DeleteBehavior.NoAction);
  }
}