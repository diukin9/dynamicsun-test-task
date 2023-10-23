using DynamicSunTestTask.Infrastructure.Common.Enums;
using DynamicSunTestTask.Infrastructure.Common.ValueObject;
using System.Text.RegularExpressions;
using Enum = DynamicSunTestTask.Infrastructure.Common.Helpers.Enum;

namespace DynamicSunTestTask.Domain.RelationalDatabase.ValueObjects;

public partial class HorizontalVisibility : ValueObject
{
    public int HorizontalVisibilityInKilometers { get; private set; }
    public ComparisonOperators ComparisonOperator { get; private set; } = ComparisonOperators.Equal;

    public HorizontalVisibility() { }

    public HorizontalVisibility(int horizontalVisibilityInKilometers, ComparisonOperators comparisonOperator = ComparisonOperators.Equal)
    {
        HorizontalVisibilityInKilometers = horizontalVisibilityInKilometers;
        ComparisonOperator = comparisonOperator;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HorizontalVisibilityInKilometers;
        yield return ComparisonOperator;
    }
}

public partial class HorizontalVisibility
{
    public static HorizontalVisibility? FromString(string? value)
    {
        if (string.IsNullOrEmpty(value)) return null;

        value = value.Trim();

#pragma warning disable SYSLIB1045 // Преобразовать в "GeneratedRegexAttribute".
        var templates = new[] 
        { 
            (Key: "=", Regex: new Regex(@"^\d$")),
            (Key: "<", Regex: new Regex(@"^менее\s\d+")), 
            (Key: ">", Regex: new Regex(@"^более\s\d+")), 
        };
#pragma warning restore SYSLIB1045 // Преобразовать в "GeneratedRegexAttribute".

        foreach (var template in templates)
        {
            if (template.Regex.IsMatch(value))
            {
                if (int.TryParse(new string(value.Where(char.IsDigit).ToArray()), out int hv))
                {
                    var op = Enum.GetValueFromDescriptionOrDefault<ComparisonOperators>(template.Key);
                    return new HorizontalVisibility(hv, op);
                }
                break;
            }
        }
        return null;
    }

    public override string ToString()
    {
        return ComparisonOperator switch
        {
            ComparisonOperators.Equal => HorizontalVisibilityInKilometers.ToString(),
            ComparisonOperators.GreaterThanOrEqual => $"более {HorizontalVisibilityInKilometers} м",
            ComparisonOperators.LessThan => $"менее {HorizontalVisibilityInKilometers} м",
            _ => throw new NotImplementedException()
        };
    }
}
