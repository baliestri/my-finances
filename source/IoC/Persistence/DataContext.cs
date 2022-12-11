// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using MyFinances.Core.Entities;

namespace MyFinances.IoC.Persistence;

public sealed class DataContext : DbContext {
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Bank> Banks { get; set; } = null!;
  public DbSet<BankAccount> Accounts { get; set; } = null!;
  public DbSet<BankTransaction> Transactions { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
}