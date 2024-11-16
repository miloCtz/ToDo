using DotNext;

using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.CompleteTask;

internal sealed class CompleteToDoTaskCommandHandler :
        ICommandHandler<CompleteToDoTaskCommand, Unit>
{

    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteToDoTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository)
    {
        _unitOfWork = unitOfWork;
        _toDoTaskRepository = toDoTaskRepository;
    }

    public async Task<Result<Unit>> Handle(CompleteToDoTaskCommand request, CancellationToken cancellationToken)
    {
        var toDoTask = await _toDoTaskRepository.GetAsync(request.ToDoTaskId);

        if (toDoTask is null)
        {
            //TODO: error validation
            var error = new ArgumentException();
            return new Result<Unit>(error);
        }

        toDoTask.Complete();
        _toDoTaskRepository.Add(toDoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

