using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.ExceptionHandler;

public class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var details = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status400BadRequest,
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

        return true;
    }
}
