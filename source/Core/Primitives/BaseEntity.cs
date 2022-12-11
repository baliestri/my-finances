// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace MyFinances.Core.Primitives;

public abstract class BaseEntity {
  protected BaseEntity() { }

  protected BaseEntity(Guid id)
    => Id = id;

  public Guid Id { get; init; } = Guid.NewGuid();
}