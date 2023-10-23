using DynamicSunTestTask.Parsers.Abstractions.Configuration;
using DynamicSunTestTask.Parsers.Abstractions.Parser;
using DynamicSunTestTask.Parsers.Infrastructure;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static DynamicSunTestTask.Parsers.StaticData;

namespace DynamicSunTestTask.Parsers;

public class ExcelParser : BaseFileParser
{
    public ExcelParser(string filePath) : base(filePath, EXCEL_FILE_EXTENSIONS) { }

    public override IEnumerable<T> Parse<T>(IExcelParserConfiguration<T> config)
    {
        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new XSSFWorkbook(fs);

        for (var sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
        {
            var sheet = workbook.GetSheetAt(sheetIndex);

            var enumerator = sheet.GetRowEnumerator();

            while (enumerator.MoveNext() && enumerator.Current is IRow row)
            {
                if (config.SkipRows.Contains(row.RowNum)) continue;
                var model = config.Mappings.Map(row);
                if (model is not null) yield return model;
            }
        }
    }
}