using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoTask.Queries.GetToDoTask;
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
            //TODO: domain erro
            var error = new ArgumentException();
            return new Result<ToDoTaskResponse>(error);
        }

        return new ToDoTaskResponse(toDoTask.Id, toDoTask.Title, toDoTask.IsDone);
    }
}

