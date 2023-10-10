using System.Runtime.Serialization;
using Api.Domain.Exceptions;

namespace Api.Application.Exceptions;

[Serializable]
public class NotFoundException : PaylocityBaseException
{
    public NotFoundException(string message) : base(message)
    {
    }

    protected NotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
    {
    }
}
