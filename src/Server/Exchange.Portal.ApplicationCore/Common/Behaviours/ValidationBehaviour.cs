using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Common.Behaviours;

internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        IEnumerable<Task<ValidationResult>> validationActions =
            _validators.Select(x => x.ValidateAsync(request, cancellationToken));

        ValidationResult[] results = await Task.WhenAll(validationActions);
        ValidationFailure[] errorMessages = results
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToArray();

        if (!errorMessages.Any())
        {
            return await next();
        }

        return InstantiateResponse(errorMessages);
    }

    private static TResponse InstantiateResponse(IEnumerable<ValidationFailure> errorMessages)
    {
        //Check whether 'Response' is of type Result<>
        //If not throw exception
        if (typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
        {
            throw new ArgumentException($"{typeof(TResponse)} response is not Result<>");
        }

        //Create an instance of a response which has to be of 'Result<>' type and populate it with corresponding errors.
        //Boxing!
        object result = typeof(TResponse)
            .GetConstructors()
            .First(x => x.GetParameters().Any(info => info.ParameterType == typeof(Exception)))
            .Invoke(new object[] { new ValidationException(errorMessages) });

        return (TResponse)result;
    }
}