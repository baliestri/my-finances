// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Abstractions.Persistence.Primitives;

public interface IRepository<TEntity> where TEntity : BaseEntity {
  Task<TEntity?> GetByIdAsync(Guid id);
  Task<IEnumerable<TEntity>> GetAllAsync();
  Task<TEntity> AddAsync(TEntity entity);
  Task<TEntity> UpdateAsync(TEntity entity);
  Task<TEntity> DeleteAsync(TEntity entity);
}