using DotNext;

using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.CompleteTask;

internal sealed class CompleteToDoItemCommandHandler :
        ICommandHandler<CompleteToDoItemCommand, Unit>
{

    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteToDoItemCommandHandler(IUnitOfWork unitOfWork, IToDoItemRepository ToDoItemRepository)
    {
        _unitOfWork = unitOfWork;
        _ToDoItemRepository = ToDoItemRepository;
    }

    public async Task<Result<Unit>> Handle(CompleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var ToDoItem = await _ToDoItemRepository.GetAsync(request.ToDoItemId);

        if (ToDoItem is null)
        {            
            return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
        }

        ToDoItem.Complete();
        _ToDoItemRepository.Add(ToDoItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

