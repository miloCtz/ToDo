﻿using ToDo.Application.Abstractions.Messaging;

namespace ToDo.Application.ToDoItems.Commands.CreateTask;
public sealed record CreateToDoItemCommand(string Title, bool IsDone) : ICommand<int>;
