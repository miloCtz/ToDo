using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Queries.GetItem;
public sealed class GetToDoItemByIdQueryHandler
    : IQueryHandler<GetToDoItemByIdQuery, ToDoItemResponse>
{
    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly ILogger<GetToDoItemByIdQueryHandler> _logger;

    public GetToDoItemByIdQueryHandler(IToDoItemRepository ToDoItemRepository, ILogger<GetToDoItemByIdQueryHandler> logger)
    {
        _ToDoItemRepository = ToDoItemRepository;
        _logger = logger;
    }

    public async Task<Result<ToDoItemResponse>> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var ToDoItem = await _ToDoItemRepository.GetAsync(request.Id, cancellationToken);

            if (ToDoItem is null)
            {
                return Result.Failure<ToDoItemResponse>(DomainErrors.ToDoList.NotFound(request.Id));
            }

            return new ToDoItemResponse(ToDoItem.Id, ToDoItem.Title, ToDoItem.IsDone);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetToDoItemByIdQueryHandler.Handle Exception.");
            return Result.Failure<ToDoItemResponse>(ex);
        }
    }
}

