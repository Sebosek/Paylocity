using Api.Application.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.ExceptionWriters;

internal class NotFoundExceptionWriter : IExceptionWriter
{
    public Type Type => typeof(NotFoundException);
    
    public ObjectResult Write(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return new NotFoundObjectResult(new ProblemDetails
        {
            Title = "Not Found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound,
        });
    }
}
