namespace ToDo.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
                                      "ValidationError",
                                      "A validation problem occurred.");

        public Error[] Errors { get; }
    }
}
