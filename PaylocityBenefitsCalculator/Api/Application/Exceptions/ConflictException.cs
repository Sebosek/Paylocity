using System.Runtime.Serialization;

using Api.Domain.Exceptions;

namespace Api.Application.Exceptions;

[Serializable]
public class ConflictException : PaylocityBaseException
{
    public ConflictException(string message) : base(message)
    {
    }

    protected ConflictException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
    {
    }
}
