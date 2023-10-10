using Api.Application.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.ExceptionWriters;

internal class ConflictExceptionWriter : IExceptionWriter
{
    public Type Type => typeof(ConflictException);

    public ObjectResult Write(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return new ConflictObjectResult(new ProblemDetails
        {
            Title = "Conflict",
            Detail = exception.Message,
            Status = StatusCodes.Status409Conflict,
        });
    }
}
