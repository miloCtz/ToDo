using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.CompleteTask;
public sealed record CompleteToDoTaskCommand(int ToDoTaskId) : ICommand<Unit>;


