using DynamicSunTestTask.Parsers.Abstractions;
using DynamicSunTestTask.Parsers.Abstractions.Configuration;
using DynamicSunTestTask.Parsers.Abstractions.Mapping;

namespace DynamicSunTestTask.Parsers.Models;

public class ExcelParserConfiguration<T> : IExcelParserConfiguration<T> where T : IOutputModel
{
    public IExcelMapping<T>[] Mappings { get; }
    public int[] SkipRows { get; }

    public ExcelParserConfiguration(IExcelMapping<T>[] mappings, int[]? skipRows = null)
    {
        Mappings = mappings;
        SkipRows = skipRows ?? Array.Empty<int>();
    }
}
