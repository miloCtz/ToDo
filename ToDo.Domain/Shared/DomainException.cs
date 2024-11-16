namespace ToDo.Domain.Shared
{
    public sealed class DomainException(string code, string message) : Exception(message)
    {
        public string Code { get; init; } = code;
    }
}
