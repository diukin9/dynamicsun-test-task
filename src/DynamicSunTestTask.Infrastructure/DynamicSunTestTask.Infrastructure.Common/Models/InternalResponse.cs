using System.ComponentModel.DataAnnotations;
using DynamicSunTestTask.Infrastructure.Common.Extensions;

namespace DynamicSunTestTask.Infrastructure.Common.Models;

public class InternalResponse
{
    public bool IsSuccess { get; protected set; }
    public Exception? Exception { get; protected set; }

    public InternalResponse Success()
    {
        IsSuccess = true;
        return this;
    }

    public InternalResponse Failure(Exception? exception = null)
    {
        Exception = exception;
        IsSuccess = false;
        return this;
    }

    public InternalResponse Failure(ValidationResult validationResult)
    {
        return Failure(validationResult.GetException());
    }

    public FailureResponse? ToFailureResponse()
    {
        if (IsSuccess) return null;

        return new FailureResponse(this);
    }
}

public class InternalResponse<T> : InternalResponse
{
    public T? Data { get; protected set; }

    public InternalResponse<T> Success(T data)
    {
        Data = data;
        IsSuccess = true;
        return this;
    }

    public new InternalResponse<T> Failure(Exception? exception = null)
    {
        Exception = exception;
        IsSuccess = false;
        return this;
    }

    public new InternalResponse<T> Failure(ValidationResult validationResult)
    {
        return Failure(validationResult.GetException());
    }
}