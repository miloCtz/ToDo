﻿namespace ToDo.Domain.Shared
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);
        public static Result<T> Failure<T>(Error error) => new(default, false, error);
        public static Result<T> Failure<T>(Exception ex) => new(default, false, Error.FromException(ex));
        public static Result<T> Create<T>(T? value) => value is not null ? Success(value) : Failure<T>(Error.NullValue);
    }
}
