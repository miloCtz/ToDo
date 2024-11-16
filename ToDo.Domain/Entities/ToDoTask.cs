using ToDo.Domain.Primitives;

namespace ToDo.Domain.Entities
{
    public sealed class ToDoItem : Entity
    {
        private ToDoItem(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }
        public bool IsDone { get; private set; }

        public void Complete()
        {
            IsDone = true;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public static ToDoItem Create(string title)
        {
            return new ToDoItem(title);
        }
    }
}
