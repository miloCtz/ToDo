using FluentValidation;
using ToDo.Domain.Entities;

namespace ToDo.Application.ToDoItems.Commands.UpdateTask;

internal class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Title).MinimumLength(ToDoItem.MinLength);
        RuleFor(x => x.Title).MaximumLength(ToDoItem.MaxLength);
    }
}

