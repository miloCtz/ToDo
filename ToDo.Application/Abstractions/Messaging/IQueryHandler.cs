using DotNext;
using MediatR;

namespace ToDo.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResult>
    : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}

