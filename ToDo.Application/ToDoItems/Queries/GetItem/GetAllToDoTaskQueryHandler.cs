using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Queries.GetItem;

public sealed class GetAllToDoItemQueryHandler
    : IQueryHandler<GetAllToDoItemQuery, ToDoItemAllResponse>
{
    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly ILogger<GetAllToDoItemQueryHandler> _logger;

    public GetAllToDoItemQueryHandler(IToDoItemRepository ToDoItemRepository, ILogger<GetAllToDoItemQueryHandler> logger)
    {
        _ToDoItemRepository = ToDoItemRepository;
        _logger = logger;
    }

    public async Task<Result<ToDoItemAllResponse>> Handle(GetAllToDoItemQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var ToDoItems = await _ToDoItemRepository.GetAllAsync(cancellationToken);

            var taskResponse = ToDoItems
                .Select(task => new ToDoItemResponse(task.Id, task.Title, task.IsDone))
                .ToList();

            return new ToDoItemAllResponse(taskResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAllToDoItemQueryHandler.Handle Exception.");
            return Result.Failure<ToDoItemAllResponse>(ex);
        }
    }
}

