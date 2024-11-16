using DotNext;
using MediatR;

namespace ToDo.Application.Abstractions.Messaging;

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}

