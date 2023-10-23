using DynamicSunTestTask.Parsers.Abstractions;
using DynamicSunTestTask.Parsers.Abstractions.Mapping;
using DynamicSunTestTask.Parsers.Models;
using NPOI.SS.UserModel;

namespace DynamicSunTestTask.Parsers.Infrastructure;

public static class ExcelMappingExtensions
{
    public static T? Map<T>(this IExcelMapping<T>[] mappings, IRow row) where T : class, IOutputModel, new()
    {
        try
        {
            var model = new T();
            mappings.ToList().ForEach(x => x.Setter.Invoke(model, new ReadOnlyRow(row)));
            return model;
        }
        catch 
        {
            Console.WriteLine($"\n>> ERROR WHEN MAPPING FROM {nameof(IRow)} TO {typeof(T).Name}");
            Console.WriteLine($">> SHEET_NAME: {row.Sheet.SheetName}, ROW_NUM: {row.RowNum}\n");
            return null; 
        }
    }
}
