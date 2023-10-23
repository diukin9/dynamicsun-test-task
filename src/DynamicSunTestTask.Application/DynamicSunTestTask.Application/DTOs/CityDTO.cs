using DynamicSunTestTask.Infrastructure.Common.Interfaces;

namespace DynamicSunTestTask.Application.DTOs;

public class CityDTO : IDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double TimeZoneRelativeToUTC { get; set; }
}
