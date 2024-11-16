using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoTasks.Queries.GetToDoTask;
internal sealed class GetTodoTaskByIdQueryHandler
    : IQueryHandler<GetToDoTaskByIdQuery, ToDoTaskResponse>
{
    private readonly IToDoTaskRepository _toDoTaskRepository;

    public GetTodoTaskByIdQueryHandler(IToDoTaskRepository toDoTaskRepository) => _toDoTaskRepository = toDoTaskRepository;

    public async Task<Result<ToDoTaskResponse>> Handle(GetToDoTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var toDoTask = await _toDoTaskRepository.GetAsync(request.Id, cancellationToken);

        if (toDoTask is null)
        {
            return new Result<ToDoTaskResponse>(DomainErrors.ToDoList.NotFound(request.Id));
        }

        return new ToDoTaskResponse(toDoTask.Id, toDoTask.Title, toDoTask.IsDone);
    }
}

