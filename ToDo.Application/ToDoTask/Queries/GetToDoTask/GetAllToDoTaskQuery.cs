using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoTask.Queries.GetToDoTask;

public sealed record GetAllToDoTaskQuery() : IQuery<ToDoTaskAllResponse>;

