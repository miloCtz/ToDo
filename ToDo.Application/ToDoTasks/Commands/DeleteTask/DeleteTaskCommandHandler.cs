using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.DeleteTask;
public sealed class DeleteTaskCommandHandler
    : ICommandHandler<DeleteTaskCommand, Unit>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository)
    {
        _unitOfWork = unitOfWork;
        _toDoTaskRepository = toDoTaskRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var toDoTask = await _toDoTaskRepository.GetAsync(request.ToDoTaskId);

        if (toDoTask is null)
        {
            return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoTaskId));
        }

        _toDoTaskRepository.Delete(toDoTask);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

