using MediatR;
using ToDo.Domain.Shared;

namespace ToDo.Application.Abstractions.Messaging;

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}

