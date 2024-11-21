using ToDo.Domain.Shared;

namespace ToDo.Domain.Errors
{
    public class DomainErrors
    {
        public static class ToDoList
        {
            public static readonly Func<int, Error> NotFound = id => new(
                "ToDoItem.NotFound",
                $"The To-Do item with id {id} was not found.");          
        }
    }
}
