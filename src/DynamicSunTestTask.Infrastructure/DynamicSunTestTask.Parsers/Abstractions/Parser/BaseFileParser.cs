using DynamicSunTestTask.Parsers.Abstractions.Configuration;
using static DynamicSunTestTask.Parsers.StaticData;

namespace DynamicSunTestTask.Parsers.Abstractions.Parser;

public abstract class BaseFileParser : IParser
{
    protected string filePath;

    public BaseFileParser(string filePath, string[]? allowedFileExtensions = default)
    {
        var file = FindFileByPath(filePath) ?? throw new FileNotFoundException();

        if ((allowedFileExtensions?.Any() ?? false) && !IsValidFileExtension(file, allowedFileExtensions))
        {
            throw UnresolvedFileExtension(allowedFileExtensions);
        }

        this.filePath = filePath;
    }

    private static FileInfo? FindFileByPath(string filePath)
    {
        if (string.IsNullOrEmpty(filePath?.Trim())) throw PathIsEmptyOrNull;

        var file = new FileInfo(filePath);

        return file.Exists ? file : default;
    }

    private static bool IsValidFileExtension(FileInfo file, string[] allowedFileExtensions)
    {
        return allowedFileExtensions.Select(x => x.ToLower()).Contains(file.Extension.ToLower());
    }

    public abstract IEnumerable<T> Parse<T>(IExcelParserConfiguration<T> config) where T : class, IOutputModel, new();
}
