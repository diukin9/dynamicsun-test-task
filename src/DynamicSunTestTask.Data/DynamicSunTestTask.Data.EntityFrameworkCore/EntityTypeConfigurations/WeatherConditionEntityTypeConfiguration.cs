using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicSunTestTask.Data.EntityFrameworkCore.EntityTypeConfigurations;

public class WeatherConditionEntityTypeConfiguration : IEntityTypeConfiguration<WeatherConditions>
{
    public void Configure(EntityTypeBuilder<WeatherConditions> builder)
    {
        builder.OwnsOne(o => o.HorizontalVisibility);
    }
}
