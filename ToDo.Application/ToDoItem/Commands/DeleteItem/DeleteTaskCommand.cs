
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.DeleteTask;

public sealed record DeleteTaskCommand(int ToDoItemId) : ICommand<Unit>;
