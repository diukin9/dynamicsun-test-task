using DynamicSunTestTask.Parsers.Abstractions;
using NPOI.SS.UserModel;

namespace DynamicSunTestTask.Parsers.Models;

internal class ReadOnlyCell : IReadOnlyCell
{
    private readonly ICell cell;

    public ReadOnlyCell(ICell cell)
    {
        this.cell = cell;
    }

    public CellType CellType => cell.CellType;

    public string StringCellValue
    {
        get
        {
            return cell.CellType switch
            {
                CellType.Numeric => cell.NumericCellValue.ToString().Trim(),
                CellType.Boolean => cell.BooleanCellValue.ToString().Trim(),
                CellType.String => cell.StringCellValue.ToString().Trim(),
                CellType.Blank => string.Empty,
                _ => throw new NotImplementedException()
            };
        }
    }

    public double? NumericCellValue
    {
        get => string.IsNullOrEmpty(StringCellValue) ? null : cell.NumericCellValue;
    }

    public bool? BooleanCellValue
    {
        get => string.IsNullOrEmpty(StringCellValue) ? null : cell.BooleanCellValue;
    }

    public DateTime? DateTimeCellValue
    {
        get
        {
            try { return cell.DateCellValue; }
            catch { if (DateTime.TryParse(cell.StringCellValue, out var value)) return value; }
            return null;
        }
    }

    public DateOnly? DateOnlyCellValue
    {
        get
        {
            var date = DateTimeCellValue;
            if (date is null) return null;
            return new DateOnly(date.Value.Year, date.Value.Month, date.Value.Day);
        }
    }

    public TimeOnly? TimeOnlyCellValue
    {
        get
        {
            var date = DateTimeCellValue;
            if (date is null) return null;
            return new(date.Value.TimeOfDay.Ticks);
        }
    }

    public TEnum EnumCellValue<TEnum, TType>() where TEnum : System.Enum
    {
        return GetEnumFromString<TEnum, TType>(StringCellValue);
    }

    public TEnum[] EnumCellValues<TEnum, TType>(string separator) where TEnum : System.Enum
    {
        return StringCellValue?.Split(separator)
            ?.Select(x => GetEnumFromString<TEnum, TType>(x))
            ?.Where(x => x is not null)
            ?.ToArray()!;
    }

    private static TEnum GetEnumFromString<TEnum, TType>(string str) where TEnum : System.Enum
    {
        return DynamicSunTestTask.Infrastructure.Common.Helpers.Enum.GetValueFromDescriptionOrDefault<TEnum>(str);
    }
}
