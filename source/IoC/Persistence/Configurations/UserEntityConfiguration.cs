// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinances.Core.Entities;

namespace MyFinances.IoC.Persistence.Configurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder.HasKey(user => user.Id);
    builder
      .Property(user => user.Email)
      .HasMaxLength(200)
      .IsRequired();
    builder
      .Property(user => user.Username)
      .HasMaxLength(50)
      .IsRequired();
    builder
      .Property(user => user.Password)
      .HasMaxLength(300)
      .IsRequired();
    builder
      .Property(user => user.FirstName)
      .HasMaxLength(50)
      .IsRequired();
    builder
      .Property(user => user.LastName)
      .HasMaxLength(50)
      .IsRequired();

    builder
      .HasMany(user => user.BankAccounts)
      .WithOne(account => account.User)
      .HasConstraintName("FK_User_BankAccount")
      .OnDelete(DeleteBehavior.NoAction);
  }
}