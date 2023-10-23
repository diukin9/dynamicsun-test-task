using DynamicSunTestTask.Data.EntityFrameworkCore.EntityTypeConfigurations;
using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTestTask.Data.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<WeatherConditions> WeatherConditions { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new WeatherConditionEntityTypeConfiguration());
        builder.ApplyConfiguration(new CityEntityTypeConfiguration());

        base.OnModelCreating(builder);
    }
}
