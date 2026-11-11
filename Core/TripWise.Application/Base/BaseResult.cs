namespace TripWise.Application.Common.Results;

public sealed class Result<T>
{
    private Result(bool isSuccess,T? data,string? errorCode,IReadOnlyList<string> errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorCode = errorCode;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T? Data { get; }

    public string? ErrorCode { get; }

    public IReadOnlyList<string> Errors { get; }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true,data,null,Array.Empty<string>());
    }

    public static Result<T> Fail(string message)
    {
        return Fail("Failure", new[] { message });
    }

    public static Result<T> Fail(string errorCode,IEnumerable<string> errors)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorCode);
        ArgumentNullException.ThrowIfNull(errors);

        var errorList = errors.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        if (errorList.Length == 0) throw new ArgumentException("En az bir hata mesajı gereklidir.");

        return new Result<T>(false,default,errorCode,errorList);
    }

    public static Result<T> NotFound(string message)
    {
        return Fail("NotFound", new[] { message });
    }
}