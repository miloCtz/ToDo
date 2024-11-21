using ToDo.Domain.Primitives;

namespace ToDo.Domain.Entities
{
    public sealed class ToDoItem : Entity
    {
        public const int MaxLength = 50;
        public const int MinLength = 2;

        private ToDoItem(string title)
        {
            Title = title;
        }

        public ToDoItem()
        {            
        }

        public string Title { get; private set; }
        public bool IsDone { get; private set; }

        public void Toggle()
        {
            IsDone = !IsDone;
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
