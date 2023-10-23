namespace DynamicSunTestTask.Parsers;

internal static class StaticData
{
    #region exceptions

    public static FormatException UnresolvedFileExtension(string[]? allowedFileExtensions = null) => new($"The file has an invalid extension. {((allowedFileExtensions?.Any() ?? false) ? $"Expected: {string.Join(", ", allowedFileExtensions!)}." : string.Empty)}");

    public static ArgumentException PathIsEmptyOrNull => new($"The file path was empty or null");

    #endregion

    #region valid excel file extensions

    public static string[] EXCEL_FILE_EXTENSIONS => new[] { XLS, XLSX };

    public const string XLS = ".xls";

    public const string XLSX = ".xlsx";

    #endregion
}
