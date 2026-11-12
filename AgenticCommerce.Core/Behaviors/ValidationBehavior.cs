using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Exceptions;
using MediatR;

namespace AgenticCommerce.Core.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IRequestValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errors = validators.SelectMany(validator => validator.Validate(request))
            .GroupBy(error => error.Key)
            .ToDictionary(group => group.Key, group => group.SelectMany(error => error.Value).Distinct().ToArray());

        if (errors.Count > 0)
            throw new RequestValidationException(errors);

        return next();
    }
}
