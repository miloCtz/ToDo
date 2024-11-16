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
        private readonly IToDoItemRepository _ToDoItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHanlder(IToDoItemRepository ToDoItemRepository, IUnitOfWork unitOfWork)
        {
            _ToDoItemRepository = ToDoItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var ToDoItem = await _ToDoItemRepository.GetAsync(request.ToDoItemId);

            if (ToDoItem is null)
            {
                return new Result<Unit>(DomainErrors.ToDoList.NotFound(request.ToDoItemId));
            }

            ToDoItem.UpdateTitle(ToDoItem.Title);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
