using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.CompleteTask;
public sealed record CompleteToDoItemCommand(int ToDoItemId) : ICommand<Unit>;


