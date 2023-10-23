using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using DynamicSunTestTask.Infrastructure.Common.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicSunTestTask.Data.EntityFrameworkCore.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    private readonly ApplicationDbContext context;

    public CityRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public override async Task<List<City>> FindAsync(Expression<Func<City, bool>> selector, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var query = context.Cities.Where(selector);
        if (!isTracked) query = query.AsNoTracking();
        var cities = await query.ToListAsync(cancellationToken: cancellationToken);
        return cities;
    }

    public override async Task<City?> FindByIdAsync(int id, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var cities = await FindAsync(x => x.Id == id, isTracked, cancellationToken);
        return cities.SingleOrDefault();
    }

    public override async Task<List<City>> FindAsync(Expression<Func<City, bool>> filter, Expression<Func<City, object>> orderBy, bool isAscending = true, int? skip = null, int? limit = null, bool isTracked = true, CancellationToken cancellationToken = default)
    {
        var query = context.Cities.Where(filter);
        query = isAscending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        if (skip.HasValue) query = query.Skip(skip.Value);
        if (limit.HasValue) query = query.Take(limit.Value);
        if (!isTracked) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}
