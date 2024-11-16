using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories;

namespace ToDo.Application.ToDoItems.Commands.CreateTask;
internal sealed class CreateToDoItemCommandHandler
    : ICommandHandler<CreateToDoItemCommand, int>
{

    private readonly IToDoItemRepository _ToDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoItemCommandHandler(IUnitOfWork unitOfWork, IToDoItemRepository ToDoItemRepository)
    {
        _unitOfWork = unitOfWork;
        _ToDoItemRepository = ToDoItemRepository;
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
            return new Result<int>(ex);
        }
    }
}

