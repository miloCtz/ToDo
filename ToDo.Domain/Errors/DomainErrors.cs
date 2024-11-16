using ToDo.Domain.Shared;

namespace ToDo.Domain.Errors
{
    public class DomainErrors
    {
        public static class ToDoList
        {
            public static readonly Func<int, DomainException> NotFound = id => new(
                "ToDoListTask.NotFound", 
                $"The To-Do task with id {id} was not found.");
        }
    }
}
