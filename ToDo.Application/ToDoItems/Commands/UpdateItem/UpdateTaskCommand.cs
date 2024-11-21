using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.UpdateItem;

public sealed record UpdateTaskCommand(int ToDoItemId, string Title) : ICommand<Unit>;

