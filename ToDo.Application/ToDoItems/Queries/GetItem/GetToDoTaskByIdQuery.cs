using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoItems.Queries.GetItem;

public sealed record GetToDoItemByIdQuery(int Id) : IQuery<ToDoItemResponse>;

