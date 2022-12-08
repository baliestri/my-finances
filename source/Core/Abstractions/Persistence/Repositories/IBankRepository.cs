// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Abstractions.Persistence.Primitives;
using MyFinances.Core.Entities;

namespace MyFinances.Core.Abstractions.Persistence.Repositories;

public interface IBankRepository : IRepository<Bank> {
  Task<Bank?> GetByNameAsync(string name);
}

