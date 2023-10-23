using DynamicSunTestTask.Application.DTOs;
using DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using DynamicSunTestTask.Domain.RelationalDatabase.Enums;
using DynamicSunTestTask.Domain.RelationalDatabase.ValueObjects;
using DynamicSunTestTask.Infrastructure.Common.Extensions;
using Mapster;
using Enum = DynamicSunTestTask.Infrastructure.Common.Helpers.Enum;

namespace DynamicSunTestTask.Application.MapsterConfigs;

public class WeatherConditionsRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WeatherConditions, WeatherConditionsDTO>()
            .Map(dest => dest.HorizontalVisibility, src => src.HorizontalVisibility! == null! ? string.Empty : src.HorizontalVisibility.ToString())
            .Map(dest => dest.LocalDate, src => src.DateTimeUTC.AddHours(src.City.TimeZoneRelativeToUTC).ToDateOnly())
            .Map(dest => dest.LocalTime, src => src.DateTimeUTC.AddHours(src.City.TimeZoneRelativeToUTC).ToTimeOnly())
            .Map(dest => dest.WindDirection, src => string.Join(", ", src.WindDirection))
            .Map(dest => dest.DateUTC, src => src.DateTimeUTC.ToDateOnly())
            .Map(dest => dest.TimeUTC, src => src.DateTimeUTC.ToTimeOnly())
            .Map(dest => dest.City, src => src.City);

        config.NewConfig<WeatherConditionsDTO, WeatherConditions>()
            .Ignore(src => src.Id)
            .Ignore(src => src.FileName)
            .Ignore(src => src.City)
            .Map(dest => dest.CityId, src => src.City.Id)
            .Map(dest => dest.WindDirection, src => Enum.GetValuesFromStringOrDefault<WindDirection>(src.WindDirection, ", ", true).Distinct().ToArray())
            .Map(dest => dest.HorizontalVisibility, src => HorizontalVisibility.FromString(src.HorizontalVisibility))
            .Map(dest => dest.DateTimeUTC, src => DateTimeExtensions.Create(src.LocalDate, src.LocalTime).ShiftTimeZone(src.City.TimeZoneRelativeToUTC));
    }
}
