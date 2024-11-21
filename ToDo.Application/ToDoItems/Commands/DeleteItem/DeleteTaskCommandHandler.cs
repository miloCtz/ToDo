using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.DeleteItem;
public sealed class DeleteTaskCommandHandler
    : ICommandHandler<DeleteTaskCommand, Unit>
{
    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;
    public DeleteTaskCommandHandler(
        IUnitOfWork unitOfWork,
        IToDoItemRepository ToDoItemRepository,
        ILogger<DeleteTaskCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _ToDoItemRepository = ToDoItemRepository;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ToDoItem = await _ToDoItemRepository.GetAsync(request.ToDoItemId);

            if (ToDoItem is null)
            {
                return Result.Failure<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
            }

            _ToDoItemRepository.Delete(ToDoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteTaskCommandHandler.Handle Exception.");
            return Result.Failure<Unit>(ex);
        }
    }
}

