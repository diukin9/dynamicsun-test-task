namespace DynamicSunTestTask.Infrastructure.Common;

public static class Exceptions
{
    public static ArgumentException ObjectNotFound(string objName) => new($"{objName} not found");
    public static ArgumentException InvalidFileExtension => new($"One or more files had an invalid extension");
}
