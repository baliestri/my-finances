// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Primitives;
using MyFinances.Core.Primitives;

namespace MyFinances.IoC.Persistence.Primitives;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity {
  private readonly ILogger<Repository<TEntity>> _logger;

  protected Repository(ILogger<Repository<TEntity>> logger, IList<TEntity> entities) {
    _logger = logger;
    Entities = entities;
  }

  protected IList<TEntity> Entities { get; }

  public async Task<TEntity?> GetByIdAsync(Guid id) {
    _logger.LogInformation("Getting entity by id: {Id}", id);

    return await Task.FromResult(Entities.FirstOrDefault(entity => entity.Id == id));
  }

  public async Task<IEnumerable<TEntity>> GetAllAsync() {
    _logger.LogInformation("Getting all entities");

    return await Task.FromResult(Entities);
  }

  public async Task<TEntity> AddAsync(TEntity entity) {
    _logger.LogInformation("Adding entity: {Id}", entity.Id);
    Entities.Add(entity);

    return await Task.FromResult(entity);
  }

  public async Task<TEntity> UpdateAsync(TEntity entity) {
    _logger.LogInformation("Updating entity: {Id}", entity.Id);

    var entityToUpdate = Entities.FirstOrDefault(e => e.Id == entity.Id);

    if (entityToUpdate is null) {
      throw new Exception($"Entity with id {entity.Id} not found");
    }

    Entities.Remove(entityToUpdate);
    Entities.Add(entity);

    return await Task.FromResult(entity);
  }

  public async Task<TEntity> DeleteAsync(TEntity entity) {
    _logger.LogInformation("Deleting entity: {Id}", entity.Id);

    var entityToDelete = Entities.FirstOrDefault(e => e.Id == entity.Id);

    if (entityToDelete is null) {
      throw new Exception($"Entity with id {entity.Id} not found");
    }

    Entities.Remove(entityToDelete);

    return await Task.FromResult(entity);
  }
}
