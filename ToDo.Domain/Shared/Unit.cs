namespace ToDo.Domain.Shared
{
    public readonly struct Unit
    {
        private static readonly Unit _value = default(Unit);

        public static ref readonly Unit Value => ref _value;
    }
}
