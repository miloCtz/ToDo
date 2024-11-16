using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.UpdateTask;

public sealed record UpdateTaskCommand(int ToDoTaskId, string Title) : ICommand<Unit>;

