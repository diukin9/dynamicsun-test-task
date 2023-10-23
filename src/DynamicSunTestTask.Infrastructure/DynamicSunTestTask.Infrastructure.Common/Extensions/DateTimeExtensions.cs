namespace DynamicSunTestTask.Infrastructure.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateOnly ToDateOnly(this System.DateTime dateTime)
    {
        return new(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public static TimeOnly ToTimeOnly(this System.DateTime dateTime)
    {
        return new(dateTime.TimeOfDay.Ticks);
    }

    public static System.DateTime Create(DateOnly dateOnly, TimeOnly timeOnly)
    {
        return new System.DateTime(
            dateOnly.Year, dateOnly.Month, dateOnly.Day,
            timeOnly.Hour, timeOnly.Minute, timeOnly.Second);
    }

    public static System.DateTime ShiftTimeZone(this System.DateTime dateTime, double offsetInHoursRelativeToUTC)
    {
        return dateTime.AddHours(offsetInHoursRelativeToUTC);
    }
}
