namespace ToDo.Domain.Shared;

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("The value does not exist.");

    public TResult Match<TResult>(
               Func<T, TResult> success,
               Func<Result, TResult> failure) =>
               IsSuccess ? success(_value!) : failure(this);

    public void Match(
               Action<T> success,
               Action<Result> failure)
    {
        if(IsSuccess)
        {
            success(Value!);
        }
        else
        {
            failure(this);
        }
    }               

    public static implicit operator Result<T>(T? value) => Create(value);
}

