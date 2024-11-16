
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(int ToDoTaskId) : ICommand<Unit>;
