using FluentValidation;
using MediatR;
using ToDo.Domain.Shared;

namespace ToDo.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            // return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    //private static T CreateValidationResult<T>(DomainException[] errors)
    //{
    //    if(typeof(T) == typeof(Result<>))
    //    {
    //        return (ValidationResult.wi)
    //    }

    //    throw new NotImplementedException();
    //}
}

