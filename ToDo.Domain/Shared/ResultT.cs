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
               Func<Error, TResult> failure) =>
               IsSuccess ? success(_value!) : failure(Error);

    public void Match(
               Action<T> success,
               Action<Error> failure)
    {
        if(IsSuccess)
        {
            success(Value!);
        }
        else
        {
            failure(Error);
        }
    }
               

    public static implicit operator Result<T>(T? value) => Create(value);
}

