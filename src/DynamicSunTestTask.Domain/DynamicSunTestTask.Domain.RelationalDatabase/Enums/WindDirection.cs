using System.ComponentModel;

namespace DynamicSunTestTask.Domain.RelationalDatabase.Enums;

public enum WindDirection
{
    None = 0,
    [Description("штиль")] Calm,
    [Description("Ю")] South,
    [Description("ЮЗ")] Southwest,
    [Description("ЮВ")] Southeast,
    [Description("С")] North,
    [Description("СЗ")] Northwest,
    [Description("СВ")] Northeast,
    [Description("З")] West,
    [Description("В")] East
}
