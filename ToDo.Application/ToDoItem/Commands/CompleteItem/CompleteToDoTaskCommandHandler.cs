using DotNext;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.CompleteTask;

public sealed class CompleteToDoItemCommandHandler :
        ICommandHandler<CompleteToDoItemCommand, Unit>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompleteToDoItemCommandHandler> _logger;

    public CompleteToDoItemCommandHandler(
        IUnitOfWork unitOfWork,
        IToDoItemRepository ToDoItemRepository,
        ILogger<CompleteToDoItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _toDoItemRepository = ToDoItemRepository;
        _logger = logger;
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
            _logger.LogError(ex, "CompleteToDoItemCommandHandler.Handle Exception.");
            return new Result<Unit>(ex);
        }
    }
}

