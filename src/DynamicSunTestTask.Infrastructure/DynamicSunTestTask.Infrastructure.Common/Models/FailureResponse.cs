namespace DynamicSunTestTask.Infrastructure.Common.Models;

public class FailureResponse
{
    public string Message { get; } = null!;

    public FailureResponse(string message)
    {
        Message = message;
    }

    public FailureResponse(InternalResponse internalResponse)
    {
        if (internalResponse is null)
        {
            throw new ArgumentException("Internal response was null.");
        }

        if (string.IsNullOrEmpty(internalResponse.Exception?.Message))
        {
            throw new ArgumentException("Error description was not specified.");
        }

        Message = internalResponse!.Exception!.Message;
    }
}
