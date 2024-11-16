using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoTasks.Queries.GetToDoTask;

public sealed record GetAllToDoTaskQuery() : IQuery<ToDoTaskAllResponse>;

