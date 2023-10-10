using Api.Application.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.ExceptionWriters;

internal class BadRequestExceptionWriter : IExceptionWriter
{
    public Type Type => typeof(BadRequestException);

    public ObjectResult Write(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return new BadRequestObjectResult(new ProblemDetails
        {
            Title = "Bad Request",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest,
        });
    }
}
