using Master.Data.Core.RequestResponse.Common.Extensions;

namespace Master.Data.Core.RequestResponse.Common.Queries;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(string error, bool isSuccess)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();

        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result Fail(string message) => new(message, false);

    public static Result<T> Fail<T>(string message) => new(default, false, message);

    public static Result Ok() => new(string.Empty, true);

    public static Result<T> Ok<T>(T value) => new(value, true, string.Empty);

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
                return result;
        }

        return Ok();
    }

    public static Result Combine(ICollection<Result> results)
    {
        var errors = results.Where(c => c.IsFailure).Select(c => c.Error).ToList();

        if (errors.IsEmpty())
            return Ok();

        return Fail(string.Join('|', errors));
    }

    public static Result<TData> Combine<TData>(ICollection<Result> results, TData data)
    {
        var errors = results.Where(c => c.IsFailure).Select(c => c.Error).ToList();

        if (errors.IsEmpty())
            return Ok(data);

        return Fail<TData>(string.Join('|', errors));
    }
}

public class Result<T> : Result
{
    private readonly T _value;
    public T Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException();

            return _value;
        }
    }
    protected internal Result(T value, bool isSuccess, string error) : base(error, isSuccess)
    {
        _value = value;
    }
}