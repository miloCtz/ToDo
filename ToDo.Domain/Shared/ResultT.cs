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
    public static implicit operator Result<T>(T? value) => Create(value);
}

