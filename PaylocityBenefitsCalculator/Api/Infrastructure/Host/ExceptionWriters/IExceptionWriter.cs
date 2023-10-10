using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.ExceptionWriters;

public interface IExceptionWriter
{
    public Type Type { get; }

    public ObjectResult Write(Exception exception);
}
