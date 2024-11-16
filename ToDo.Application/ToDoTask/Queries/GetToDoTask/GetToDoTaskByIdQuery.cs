using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoTask.Queries.GetToDoTask;

public sealed record GetToDoTaskByIdQuery(long Id) : IQuery<ToDoTaskResponse>;

