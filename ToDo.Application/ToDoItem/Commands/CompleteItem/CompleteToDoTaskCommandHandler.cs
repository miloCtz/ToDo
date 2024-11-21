using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.CompleteTask;

internal sealed class CompleteToDoItemCommandHandler :
        ICommandHandler<CompleteToDoItemCommand, Unit>
{

    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteToDoItemCommandHandler(IUnitOfWork unitOfWork, IToDoItemRepository ToDoItemRepository)
    {
        _unitOfWork = unitOfWork;
        _toDoItemRepository = ToDoItemRepository;
    }

    public async Task<Result<Unit>> Handle(CompleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var toDoItem = await _toDoItemRepository.GetAsync(request.ToDoItemId);

            if (toDoItem is null)
            {
                return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
            }

            toDoItem.Toggle();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            return new Result<Unit>(ex);
        }
    }
}

