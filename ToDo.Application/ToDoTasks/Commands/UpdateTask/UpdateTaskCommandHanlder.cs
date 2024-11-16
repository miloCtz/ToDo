using DotNext;
using ToDo.Application.Abstractions.Messaging;
using ToDo.Domain.Repositories;
using ToDo.Domain.Shared;

namespace ToDo.Application.ToDoTasks.Commands.UpdateTask
{
    public sealed class UpdateTaskCommandHanlder
        : ICommandHandler<UpdateTaskCommand, Unit>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHanlder(IToDoTaskRepository toDoTaskRepository, IUnitOfWork unitOfWork)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var toDoTask = await _toDoTaskRepository.GetAsync(request.ToDoTaskId);

            if (toDoTask is null)
            {
                //TODO: error validation
                var error = new ArgumentException();
                return new Result<Unit>(error);
            }

            toDoTask.UpdateTitle(toDoTask.Title);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
