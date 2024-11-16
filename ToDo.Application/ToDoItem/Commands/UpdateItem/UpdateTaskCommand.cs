using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.UpdateTask;

public sealed record UpdateTaskCommand(int ToDoItemId, string Title) : ICommand<Unit>;

