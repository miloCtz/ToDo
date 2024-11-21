using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoItems.Commands.CreateItem;

public sealed record CreateToDoItemCommand(string Title) : ICommand<int>;
