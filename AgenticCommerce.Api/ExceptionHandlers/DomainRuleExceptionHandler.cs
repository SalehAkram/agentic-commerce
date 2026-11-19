using AgenticCommerce.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AgenticCommerce.Api.ExceptionHandlers;

public sealed class DomainRuleExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DomainRuleException domainException)
            return false;

        var problem = new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            [domainException.MemberName] = [domainException.Message]
        })
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "A business rule prevented this operation."
        };
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(problem, options: null,
            contentType: "application/problem+json", cancellationToken: cancellationToken);
        return true;
    }
}
