using System.ComponentModel.DataAnnotations;

namespace DynamicSunTestTask.Infrastructure.Common.Extensions;

public static class ValidationResultExtensions
{
    public static bool IsValid(this ValidationResult result)
    {
        return string.IsNullOrEmpty(result.ErrorMessage);
    }

    public static ValidationException? GetException(this ValidationResult result)
    {
        if (result.IsValid()) return null;
        
        return new ValidationException(result.ErrorMessage);
    }
}
