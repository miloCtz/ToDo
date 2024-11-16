using ToDo.Domain.Shared;

namespace ToDo.Domain.Errors
{
    public class DomainErrors
    {
        public static class ToDoList
        {
            public static readonly Func<int, DomainException> NotFound = id => new(
                "ToDoItem.NotFound",
                $"The To-Do item with id {id} was not found.");

            public static readonly DomainException IsAlreadyDone = new(
                "ToDoItem.IsAlreadyDone",
                "The To-Do item is alredy done.");
        }
    }
}
