using MediatR;
using ToDo.Domain.Shared;

namespace ToDo.Application.Abstractions.Messaging;
public interface IQuery<TResult> : IRequest<Result<TResult>>
{
}

