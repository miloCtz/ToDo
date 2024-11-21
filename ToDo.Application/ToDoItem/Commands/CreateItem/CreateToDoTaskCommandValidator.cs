using FluentValidation;
using ToDo.Domain.Entities;

namespace ToDo.Application.ToDoItems.Commands.CreateTask;

internal class CreateToDoTaskCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoTaskCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Title).MinimumLength(ToDoItem.MinLength);
        RuleFor(x => x.Title).MaximumLength(ToDoItem.MaxLength);
    }
}

