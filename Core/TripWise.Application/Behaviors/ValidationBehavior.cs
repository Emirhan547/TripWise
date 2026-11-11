using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace TripWise.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken)
    {
        var failures = new List<ValidationFailure>();

        foreach (var validator in _validators)
        {
            var context = new ValidationContext<TRequest>(request);

            var result = await validator.ValidateAsync(context,cancellationToken);

            failures.AddRange(result.Errors);
        }

        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}