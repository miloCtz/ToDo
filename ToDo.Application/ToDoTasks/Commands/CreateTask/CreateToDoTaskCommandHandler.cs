using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoTasks.Commands.CreateTask;
internal sealed class CreateToDoTaskCommandHandler
    : ICommandHandler<CreateToDoTaskCommand, int>
{

    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoTaskCommandHandler(IUnitOfWork unitOfWork, IToDoTaskRepository toDoTaskRepository)
    {
        _unitOfWork = unitOfWork;
        _toDoTaskRepository = toDoTaskRepository;
    }

    public async Task<Result<int>> Handle(CreateToDoTaskCommand request, CancellationToken cancellationToken)
    {
        var toDoTask = ToDoTask.Create(request.Title);

        _toDoTaskRepository.Add(toDoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return toDoTask.Id;
    }
}

