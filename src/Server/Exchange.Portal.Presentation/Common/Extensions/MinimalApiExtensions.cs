using FluentValidation;

namespace Exchange.Portal.Presentation.Common.Extensions;

internal static class MinimalApiExtensions
{
    internal static IResult ToOk<TResult>(this Result<TResult> result)
    {
        return result.Match(successObj =>
            {
                if (typeof(TResult) == typeof(Unit))
                {
                    return Results.Ok();
                }
                
                return Results.Ok(successObj);
            },
            ToBadRequest);
    }

    private static IResult ToBadRequest(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return Results.ValidationProblem(validationException.ToDictionary());
        }

        return Results.StatusCode(500);
    }

    private static IDictionary<string, string[]> ToDictionary(this ValidationException validationException)
    {
        return validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}