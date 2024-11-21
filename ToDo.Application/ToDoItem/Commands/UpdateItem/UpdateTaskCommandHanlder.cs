using DotNext;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UpdateTaskCommandHanlder> _logger;
        public UpdateTaskCommandHanlder(
            IToDoItemRepository ToDoItemRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<UpdateTaskCommandHanlder> logger)
        {
            _toDoItemRepository = ToDoItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogError(ex, "UpdateTaskCommandHanlder.Handle Exception.");
                return new Result<Unit>(ex);
            }
        }
    }
}
