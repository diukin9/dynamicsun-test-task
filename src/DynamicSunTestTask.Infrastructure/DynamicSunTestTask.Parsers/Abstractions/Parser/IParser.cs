using DynamicSunTestTask.Parsers.Abstractions.Configuration;

namespace DynamicSunTestTask.Parsers.Abstractions.Parser;

public interface IParser
{
    public IEnumerable<T> Parse<T>(IExcelParserConfiguration<T> config) where T : class, IOutputModel, new();
}
