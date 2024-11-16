using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.DeleteTask;
public sealed class DeleteTaskCommandHandler
    : ICommandHandler<DeleteTaskCommand, Unit>
{
    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork, IToDoItemRepository ToDoItemRepository)
    {
        _unitOfWork = unitOfWork;
        _ToDoItemRepository = ToDoItemRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ToDoItem = await _ToDoItemRepository.GetAsync(request.ToDoItemId);

            if (ToDoItem is null)
            {
                return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
            }

            _ToDoItemRepository.Delete(ToDoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return new Result<Unit>(ex);
        }
    }
}

