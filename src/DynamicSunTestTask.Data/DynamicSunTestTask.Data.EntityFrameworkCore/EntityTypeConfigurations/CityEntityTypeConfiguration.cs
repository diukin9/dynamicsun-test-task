using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicSunTestTask.Data.EntityFrameworkCore.EntityTypeConfigurations;

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(nameof(City.Name)).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
