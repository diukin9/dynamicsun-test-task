using DynamicSunTestTask.Parsers.Abstractions;
using DynamicSunTestTask.Parsers.Abstractions.Mapping;

namespace DynamicSunTestTask.Parsers.Models;

public class ExcelMapping<T> : IExcelMapping<T>
{
    public Action<T, IReadOnlyRow> Setter { get; }

    public ExcelMapping(Action<T, IReadOnlyRow> setter)
    {
        Setter = setter ?? throw new ArgumentNullException();
    }
}
