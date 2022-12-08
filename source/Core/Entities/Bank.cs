// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Entities;

public sealed class Bank : BaseEntity {
  public string Name { get; set; } = null!;

  public IEnumerable<BankAccount> Accounts { get; set; } = null!;
}
