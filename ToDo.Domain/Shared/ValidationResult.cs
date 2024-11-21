namespace ToDo.Domain.Shared
{
    public sealed class ValidationResult<TResult> : Result<TResult>
    {
        public static readonly Error ValidationError = new(
                                        "ValidationError",
                                        "A validation problem occurred.");

        private ValidationResult(Error[] errors) : base(default, false, ValidationError)
        {
            Errors = errors;
        }

        public Error[] Errors { get; }
        public static ValidationResult<TResult> WithErrors(Error[] errors) => new(errors);
    }
}
