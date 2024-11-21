using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.DeleteItem;

public sealed record DeleteTaskCommand(int ToDoItemId) : ICommand<Unit>;
