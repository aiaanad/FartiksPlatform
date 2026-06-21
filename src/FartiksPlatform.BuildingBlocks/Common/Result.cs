using FartiksPlatform.BuildingBlocks.Errors;

namespace FartiksPlatform.BuildingBlocks.Common;

public class Result
{
    protected Result(bool isSuccess, ErrorType error)
    {
        if (isSuccess && error is string.Empty)
        {
            throw new InvalidOperationException("Success result cannot contain an error.");
        }

        if (!isSuccess && error is string.Empty)
        {
            throw new InvalidOperationException("Failure result must contain an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ErrorType Error { get; }

    public static Result Success() => new(true, string.Empty);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, string.Empty);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, ErrorType error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access value of a failed result.");
}
