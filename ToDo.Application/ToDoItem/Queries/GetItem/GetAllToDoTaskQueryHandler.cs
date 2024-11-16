using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoItems.Queries.GetToDoItem;

internal sealed class GetAllToDoItemQueryHandler
    : IQueryHandler<GetAllToDoItemQuery, ToDoItemAllResponse>
{
    private readonly IToDoItemRepository _ToDoItemRepository;

    public GetAllToDoItemQueryHandler(IToDoItemRepository ToDoItemRepository) => _ToDoItemRepository = ToDoItemRepository;

    public async Task<Result<ToDoItemAllResponse>> Handle(GetAllToDoItemQuery request, CancellationToken cancellationToken)
    {
        var ToDoItems = await _ToDoItemRepository.GetAllAsync(cancellationToken);

        if (ToDoItems is null)
        {
            //TODO: Domain exceptions
            var error = new ArgumentException();
            return new Result<ToDoItemAllResponse>(error);
        }

        var taskResponse = ToDoItems
            .Select(task => new ToDoItemResponse(task.Id, task.Title, task.IsDone))
            .ToList();

        return new ToDoItemAllResponse(taskResponse);
    }
}

