using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoItems.Queries.GetToDoItem;

public sealed record GetToDoItemByIdQuery(int Id) : IQuery<ToDoItemResponse>;

