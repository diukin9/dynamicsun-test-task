using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using DynamicSunTestTask.Infrastructure.Common.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DynamicSunTestTask.Data.EntityFrameworkCore.Repositories;

public class WeatherConditionsRepository : Repository<WeatherConditions>, IWeatherConditionsRepository
{
    private readonly ApplicationDbContext context;

    public WeatherConditionsRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public override async Task<List<WeatherConditions>> FindAsync(Expression<Func<WeatherConditions, bool>> filter, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var query = context.WeatherConditions.Include(x => x.City).Where(filter);
        if (!isTracked) query = query.AsNoTracking();
        var conditions = await query.ToListAsync(cancellationToken: cancellationToken);
        return conditions;
    }

    public override async Task<WeatherConditions?> FindByIdAsync(int id, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var conditions = await FindAsync(x => x.Id == id, isTracked, cancellationToken);
        return conditions.SingleOrDefault();
    }
    public override async Task<List<WeatherConditions>> FindAsync(Expression<Func<WeatherConditions, bool>> filter, Expression<Func<WeatherConditions, object>> orderBy, bool isAscending = true, int? skip = null, int? limit = null, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var query = context.WeatherConditions.Include(x => x.City).Where(filter);
        query = isAscending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        if (skip.HasValue) query = query.Skip(skip.Value);
        if (limit.HasValue) query = query.Take(limit.Value);
        if (!isTracked) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken:cancellationToken);
    }
}
