using MediatR;
using ToDo.Domain.Shared;

namespace ToDo.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResult>
    : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}

