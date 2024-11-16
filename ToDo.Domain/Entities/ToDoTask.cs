using ToDo.Domain.Primitives;

namespace ToDo.Domain.Entities
{
    public sealed class ToDoTask : Entity
    {
        private ToDoTask(long id, string title)
            : base(id)
        {
            Title = title;
        }

        public string Title { get; private set; }
        public bool IsDone { get; private set; }

        public void Complete()
        {
            IsDone = true;
        }

        public static ToDoTask Create(long id, string title)
        {
            return new ToDoTask(id, title);
        }
    }
}
