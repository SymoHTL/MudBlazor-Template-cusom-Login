namespace Domain.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class {
    Task<TEntity?> ReadAsync(int id, CancellationToken ct);
    Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
    Task<List<TEntity>> ReadAsync(CancellationToken ct);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct);
    Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken ct);
    Task UpdateAsync(TEntity entity, CancellationToken ct);
    Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct);
    Task DeleteAsync(TEntity entity, CancellationToken ct);
    Task DeleteAsync(IEnumerable<TEntity> entity, CancellationToken ct);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
}