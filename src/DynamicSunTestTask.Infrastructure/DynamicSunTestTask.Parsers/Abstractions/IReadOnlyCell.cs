using NPOI.SS.UserModel;

namespace DynamicSunTestTask.Parsers.Abstractions;

public interface IReadOnlyCell
{
    public CellType CellType { get; }
    public string StringCellValue { get; }
    public DateTime? DateTimeCellValue { get; }
    public DateOnly? DateOnlyCellValue { get; }
    public TimeOnly? TimeOnlyCellValue { get; }
    public double? NumericCellValue { get; }
    public bool? BooleanCellValue { get; }
    public TEnum EnumCellValue<TEnum, TType>() where TEnum : Enum;
    public TEnum[] EnumCellValues<TEnum, TType>(string separator) where TEnum : Enum;
}