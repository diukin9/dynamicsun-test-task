using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DynamicSunTestTask.Infrastructure.Common.Interfaces;

namespace DynamicSunTestTask.Infrastructure.Common.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IIdentified, new()
{
    private readonly DbContext context;

    public Repository(DbContext context)
    {
        this.context = context;
    }

    public abstract Task<TEntity?> FindByIdAsync(int id, bool isTracked = true, CancellationToken cancellationToken = default);

    public abstract Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector, bool isTracked = true, CancellationToken cancellationToken = default);

    public abstract Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, bool isAscending = true, int? skip = null, int? limit = null, bool isTracked = true, CancellationToken cancellationToken = default);

    public virtual IQueryable<TEntity> All()
    {
        return context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> AllAsync(bool isTracked = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (!isTracked) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> AllAsync(Expression<Func<TEntity, object>> orderBy, bool isAscending = true, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (!isTracked) query = query.AsNoTracking();
        query = isAscending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().AsNoTracking().AnyAsync(selector, cancellationToken);
    }

    public virtual void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().AsNoTracking().Where(filter).CountAsync(cancellationToken);
    }
}
