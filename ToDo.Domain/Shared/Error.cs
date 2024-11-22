namespace ToDo.Domain.Shared
{
    public record class Error
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");               

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(Exception ex)
        {
            Code = ex.GetType().Name;
            Message = ex.Message;
            Exception = ex;
        }

        public string Code { get; init; }
        public string Message { get; init; }
        public Exception? Exception { get; init; }
        public bool IsException => Exception is not null;
        public static Error FromException(Exception ex) => new(ex);
    }
}
