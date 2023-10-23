using DynamicSunTestTask.Parsers.Abstractions.Mapping;

namespace DynamicSunTestTask.Parsers.Abstractions.Configuration;

public interface IExcelParserConfiguration<T> : IParserConfiguration
{
    public IExcelMapping<T>[] Mappings { get; }
    public int[] SkipRows { get; }
}
