using DynamicSunTestTask.Infrastructure.Common.Interfaces;

namespace DynamicSunTestTask.Domain.RelationalDatabase.Entities;

public class City : IIdentified
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double TimeZoneRelativeToUTC { get; set; }
}
