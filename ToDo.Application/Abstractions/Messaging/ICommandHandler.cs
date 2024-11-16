using DotNext;
using MediatR;

namespace ToDo.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand, TResult>
    : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}

