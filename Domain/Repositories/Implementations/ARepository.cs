namespace Domain.Repositories.Implementations;

public abstract class ARepository<TEntity> : IRepository<TEntity> where TEntity : class {
    private readonly ModelDbContext _context;
    protected readonly DbSet<TEntity> Set;

    protected ARepository(ModelDbContext context) {
        _context = context;
        Set = _context.Set<TEntity>();
    }

    public async Task<List<TEntity>> ReadAsync(CancellationToken ct) {
        return await Set.ToListAsync(ct);
    }

    public async Task<TEntity?> ReadAsync(int id, CancellationToken ct) {
        return await Set.FindAsync(new object?[] {id}, ct);
    }

    public async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        return await Set.Where(filter).ToListAsync(ct);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct) {
        Set.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken ct) {
        Set.AddRange(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct) {
        Set.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
        Set.UpdateRange(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct) {
        Set.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
        Set.RemoveRange(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        Set.RemoveRange(Set.Where(filter));
        await _context.SaveChangesAsync(ct);
    }
}