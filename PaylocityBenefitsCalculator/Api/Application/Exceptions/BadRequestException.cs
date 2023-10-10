using System.Runtime.Serialization;

using Api.Domain.Exceptions;

namespace Api.Application.Exceptions;

[Serializable]
public class BadRequestException : PaylocityBaseException
{
    public BadRequestException(string message) : base(message)
    {
    }

    protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
    {
    }
}
