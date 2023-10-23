using DynamicSunTestTask.Domain.RelationalDatabase.Enums;
using DynamicSunTestTask.Domain.RelationalDatabase.ValueObjects;
using DynamicSunTestTask.Infrastructure.Common.Extensions;
using DynamicSunTestTask.Infrastructure.Common.Interfaces;

namespace DynamicSunTestTask.Domain.RelationalDatabase.Entities;

public class WeatherConditions : IIdentified
{
    public int Id { get; set; }

    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;

    public DateTime DateTimeUTC { get; set; }
    public DateTime LocalDateTime => DateTimeUTC.ShiftTimeZone(City.TimeZoneRelativeToUTC);

    public double? AirTemperature { get; set; }
    public int? AirRelativeHumidityAsPercentage { get; set; }
    public double? DewPointInDegreesCelsius { get; set; }
    public int? AtmosphericPressureInMmHg { get; set; }
    public List<WindDirection> WindDirection { get; set; } = null!;
    public int? WindSpeedInMetersPerSecond { get; set; }
    public int? CloudCoverAsPercentage { get; set; }
    public int? LowerCloudLimitInMeters { get; set; }
    public HorizontalVisibility? HorizontalVisibility { get; set; }
    public string? WeatherPhenomena { get; set; }

    public string FileName { get; set; } = null!;
}
