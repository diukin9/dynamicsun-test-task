using System.ComponentModel;

namespace DynamicSunTestTask.Infrastructure.Common.Enums;

public enum ComparisonOperators
{
    [Description(">")] GreaterThan,
    [Description("<")] LessThan,
    [Description(">=")] GreaterThanOrEqual,
    [Description("<=")] LessThanOrEqual,
    [Description("=")] Equal
}
