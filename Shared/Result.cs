using System.Text.Json.Serialization;

namespace Shared;

public class Result<T> : Result
{
    public T? Data { get; set; }

    [JsonIgnore]
    public bool IsErrorOrEmpty
    {
        get
        {
            return !IsOk || Data == null;
        }
    }

    public static Result<T> Ok(T? data = default)
    {
        return new Result<T>
        {
            IsOk = true,
            Data = data
        };
    }

    public static Result<T> Error(string? message = null, T? data = default)
    {
        return new Result<T>
        {
            Data = data,
            IsOk = false,
            Message = message
        };
    }
}

public class Result
{
    public bool IsOk { get; set; }
    public string? Message { get; set; }

    public Result()
    {
    }

    public Result(bool isOk, string message)
    {
        IsOk = isOk;
        Message = message;
    }

    public static Result Ok()
    {
        return new Result
        {
            IsOk = true
        };
    }

    public static Result NotFound()
    {
        return new Result
        {
            IsOk = false,
            Message = "The resource was not found"
        };
    }

    public static Result Error(string message)
    {
        return new Result
        {
            IsOk = false,
            Message = message
        };
    }
}
