using DotNext;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoItems.Commands.CreateTask;
internal sealed class CreateToDoItemCommandHandler
    : ICommandHandler<CreateToDoItemCommand, int>
{
    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateToDoItemCommandHandler> _logger;

    public CreateToDoItemCommandHandler(
        IUnitOfWork unitOfWork,
        IToDoItemRepository ToDoItemRepository,
        ILogger<CreateToDoItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _ToDoItemRepository = ToDoItemRepository;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var toDoItem = ToDoItem.Create(request.Title);
            _ToDoItemRepository.Add(toDoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return toDoItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateToDoItemCommandHandler.Handle Exception.");
            return new Result<int>(ex);
        }
    }
}

