using DynamicSunTestTask.Parsers.Abstractions;
using NPOI.SS.UserModel;

namespace DynamicSunTestTask.Parsers.Models;

internal class ReadOnlyRow : IReadOnlyRow
{
    private readonly IRow row;

    public ReadOnlyRow(IRow row)
    {
        this.row = row;
    }

    public IReadOnlyCell GetCell(int column)
    {
        return new ReadOnlyCell(row.GetCell(column) ?? row.CreateCell(column, CellType.Blank));
    }
}
