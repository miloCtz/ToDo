using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoTasks.Commands.CreateTask;
public sealed record CreateToDoTaskCommand(string Title, bool IsDone) : ICommand<int>;
