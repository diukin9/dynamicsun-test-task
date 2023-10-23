using Microsoft.AspNetCore.Http;

namespace DynamicSunTestTask.Infrastructure.Common.Helpers;

public class FileHelper
{
    public static async Task LoadAsync(string dirPath, IFormFile file, string? fileName = null)
    {
        using var fileStream = new FileStream(Path.Combine(dirPath, fileName ?? file.FileName), FileMode.Create);
        await file.CopyToAsync(fileStream);
    }
}
