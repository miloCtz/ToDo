using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoItems.Queries.GetToDoItem;
internal sealed class GetToDoItemByIdQueryHandler
    : IQueryHandler<GetToDoItemByIdQuery, ToDoItemResponse>
{
    private readonly IToDoItemRepository _ToDoItemRepository;

    public GetToDoItemByIdQueryHandler(IToDoItemRepository ToDoItemRepository) => _ToDoItemRepository = ToDoItemRepository;

    public async Task<Result<ToDoItemResponse>> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var ToDoItem = await _ToDoItemRepository.GetAsync(request.Id, cancellationToken);

        if (ToDoItem is null)
        {
            return new Result<ToDoItemResponse>(DomainErrors.ToDoList.NotFound(request.Id));
        }

        return new ToDoItemResponse(ToDoItem.Id, ToDoItem.Title, ToDoItem.IsDone);
    }
}

