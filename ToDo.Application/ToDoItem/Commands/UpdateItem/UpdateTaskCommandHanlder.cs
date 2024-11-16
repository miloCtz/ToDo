using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Errors;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoItems.Commands.UpdateTask
{
    public sealed class UpdateTaskCommandHanlder
        : ICommandHandler<UpdateTaskCommand, Unit>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHanlder(IToDoItemRepository ToDoItemRepository, IUnitOfWork unitOfWork)
        {
            _toDoItemRepository = ToDoItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ToDoItem = await _toDoItemRepository.GetAsync(request.ToDoItemId);

                if (ToDoItem is null)
                {
                    return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
                }

                ToDoItem.UpdateTitle(request.Title);
                _toDoItemRepository.Add(ToDoItem);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return new Result<Unit>(ex);
            }
        }
    }
}
