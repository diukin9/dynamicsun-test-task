namespace DynamicSunTestTask.Parsers.Abstractions.Mapping;

public interface IExcelMapping<T> : IMapping
{
    public Action<T, IReadOnlyRow> Setter { get; }
}
