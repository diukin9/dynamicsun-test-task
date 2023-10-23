using DynamicSunTestTask.Infrastructure.Common.Interfaces;
using DynamicSunTestTask.Parsers.Abstractions;

namespace DynamicSunTestTask.Application.DTOs;

public class WeatherConditionsDTO : IDTO, IOutputModel
{
    public int Id { get; set; }

    public CityDTO City { get; set; } = null!;

    public DateOnly DateUTC { get; set; }
    public TimeOnly TimeUTC { get; set; }

    public DateOnly LocalDate { get; set; }
    public TimeOnly LocalTime { get; set; }

    public double? AirTemperature { get; set; }
    public int? AirRelativeHumidityAsPercentage { get; set; }
    public double? DewPointInDegreesCelsius { get; set; }
    public int? AtmosphericPressureInMmHg { get; set; }
    public string WindDirection { get; set; } = null!;
    public int? WindSpeedInMetersPerSecond { get; set; }
    public int? CloudCoverAsPercentage { get; set; }
    public int? LowerCloudLimitInMeters { get; set; }
    public string? HorizontalVisibility { get; set; } = null!;
    public string? WeatherPhenomena { get; set; }
}
