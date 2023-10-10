using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.ExceptionWriters;

internal class ExceptionWriter : IExceptionWriter
{
    public Type Type => typeof(Exception);
    
    public ObjectResult Write(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return new ObjectResult(new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
