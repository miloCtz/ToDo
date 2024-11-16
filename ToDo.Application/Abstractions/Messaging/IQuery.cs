using DotNext;
using MediatR;

namespace ToDo.Application.Abstractions.Messaging;
public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}

