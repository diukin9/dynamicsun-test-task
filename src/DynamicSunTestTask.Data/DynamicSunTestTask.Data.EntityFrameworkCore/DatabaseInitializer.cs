using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicSunTestTask.Data.EntityFrameworkCore;

public static class DatabaseInitializer
{
    public static void Run(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        AddBaseCities(context);
    }

    public static void AddBaseCities(DbContext context)
    {
        var cities = new[]
        {
            new City { Name = "Москва", TimeZoneRelativeToUTC = 3 }
        };
        
        foreach (var city in cities) 
        {
            if (context.Set<City>().SingleOrDefault(x => x.Name == city.Name) is null) 
            {
                context.Set<City>().Add(city);
            }
        }

        context.SaveChanges();
    }
}
