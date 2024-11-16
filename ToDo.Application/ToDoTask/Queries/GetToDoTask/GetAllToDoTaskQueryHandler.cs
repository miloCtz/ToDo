using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Application.ToDoTask.Queries.GetToDoTask;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoTask.Queries.GetToDoTas;

internal sealed class GetAllToDoTaskQueryHandler
    : IQueryHandler<GetAllToDoTaskQuery, ToDoTaskAllResponse>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;

    public GetAllToDoTaskQueryHandler(IToDoTaskRepository toDoTaskRepository) => _toDoTaskRepository = toDoTaskRepository;

    public async Task<Result<ToDoTaskAllResponse>> Handle(GetAllToDoTaskQuery request, CancellationToken cancellationToken)
    {
        var toDoTasks = await _toDoTaskRepository.GetAllAsync(cancellationToken);

        if (toDoTasks is null)
        {
            //TODO: Domain exceptions
            var error = new ArgumentException();
            return new Result<ToDoTaskAllResponse>(error);
        }

        var taskResponse = toDoTasks
            .Select(task => new ToDoTaskResponse(task.Id, task.Title, task.IsDone))
            .ToList();

        return new ToDoTaskAllResponse(taskResponse);
    }
}

