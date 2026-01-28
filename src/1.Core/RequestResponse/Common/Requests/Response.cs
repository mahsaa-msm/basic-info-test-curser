using Master.Data.Core.RequestResponse.Common.Extensions;

namespace Master.Data.Core.RequestResponse.Common.Requests;

public class Response
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Response(string error, bool isSuccess)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();

        Error = error;
        IsSuccess = isSuccess;
    }

    public static Response Fail(string message) => new(message, false);

    public static Response<T> Fail<T>(string message) => new(default, false, message);

    public static Response Ok() => new(string.Empty, true);

    public static Response<T> Ok<T>(T value) => new(value, true, string.Empty);

    public static Response Combine(params Response[] results)
    {
        foreach (var result in results)
        {
            if (result.IsFailure)
                return result;
        }

        return Ok();
    }

    public static Response Combine(ICollection<Response> results)
    {
        var errors = results.Where(c => c.IsFailure).Select(c => c.Error).ToList();

        if (errors.IsEmpty())
            return Ok();

        return Fail(string.Join('|', errors));
    }

    public static Response<TData> Combine<TData>(ICollection<Response> results, TData data)
    {
        var errors = results.Where(c => c.IsFailure).Select(c => c.Error).ToList();

        if (errors.IsEmpty())
            return Ok(data);

        return Fail<TData>(string.Join('|', errors));
    }
}

public class Response<T> : Response
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
    protected internal Response(T value, bool isSuccess, string error) : base(error, isSuccess)
    {
        _value = value;
    }
}
