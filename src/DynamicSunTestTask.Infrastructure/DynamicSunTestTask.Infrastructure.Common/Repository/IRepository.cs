using System.Linq.Expressions;

namespace DynamicSunTestTask.Infrastructure.Common.Repository;

public interface IRepository<TEntity> where TEntity : class, new()
{
    public IQueryable<TEntity> All();

    public Task<List<TEntity>> AllAsync(bool isTracked = true, CancellationToken cancellationToken = default);

    public Task<List<TEntity>> AllAsync(Expression<Func<TEntity, object>> orderBy, bool isAscending = true, bool isTracked = true, CancellationToken cancellationToken = default);

    public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, bool isTracked = true, CancellationToken cancellationToken = default);

    public Task<TEntity?> FindByIdAsync(int id, bool isTracked = true, CancellationToken cancellationToken = default);

    public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, bool isAscending = true, int? skip = null, int? limit = null, bool isTracked = true, CancellationToken cancellationToken = default);

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    public void Remove(TEntity entity);

    public void RemoveRange(IEnumerable<TEntity> entities);

    public void Update(TEntity entity);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default);

    public Task<int> CountAsync(CancellationToken cancellationToken = default);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
}
