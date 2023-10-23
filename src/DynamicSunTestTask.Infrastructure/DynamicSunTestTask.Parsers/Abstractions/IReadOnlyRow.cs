namespace DynamicSunTestTask.Parsers.Abstractions;

public interface IReadOnlyRow
{
    public IReadOnlyCell GetCell(int column);
}