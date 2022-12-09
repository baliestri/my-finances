// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyFinances.Core.Abstractions.Persistence.Primitives;
using MyFinances.Core.Primitives;

namespace MyFinances.IoC.Persistence.Primitives;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity {
  private readonly DataContext _context;
  private readonly ILogger<Repository<TEntity>> _logger;

  protected Repository(ILogger<Repository<TEntity>> logger, DataContext context) {
    _logger = logger;
    _context = context;
  }

  public async Task<TEntity?> GetByIdAsync(Guid id) {
    _logger.LogInformation("Getting entity by id: {Id}", id);

    return await _context
      .Set<TEntity>()
      .FindAsync(id);
  }

  public async Task<IEnumerable<TEntity>> GetAllAsync() {
    _logger.LogInformation("Getting all entities");

    return await _context
      .Set<TEntity>()
      .ToListAsync();
  }

  public async Task<TEntity> AddAsync(TEntity entity) {
    _logger.LogInformation("Adding entity: {Id}", entity.Id);

    await _context
      .Set<TEntity>()
      .AddAsync(entity);

    await _context.SaveChangesAsync();

    return entity;
  }

  public async Task<TEntity> UpdateAsync(TEntity entity) {
    _logger.LogInformation("Updating entity: {Id}", entity.Id);

    var entityToUpdate = await GetByIdAsync(entity.Id);

    if (entityToUpdate is null) {
      throw new Exception($"Entity with id {entity.Id} not found");
    }

    _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);

    await _context.SaveChangesAsync();

    return entity;
  }

  public async Task<TEntity> DeleteAsync(TEntity entity) {
    _logger.LogInformation("Deleting entity: {Id}", entity.Id);

    var entityToDelete = await GetByIdAsync(entity.Id);

    if (entityToDelete is null) {
      throw new Exception($"Entity with id {entity.Id} not found");
    }

    _context.Set<TEntity>().Remove(entityToDelete);

    await _context.SaveChangesAsync();

    return entity;
  }
}