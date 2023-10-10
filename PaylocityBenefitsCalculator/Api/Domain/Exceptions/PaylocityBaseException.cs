using System.Runtime.Serialization;

namespace Api.Domain.Exceptions;

[Serializable]
public class PaylocityBaseException : Exception
{
    public PaylocityBaseException()
    {
    }

    public PaylocityBaseException(string message) : base(message)
    {
    }

    public PaylocityBaseException(string message, Exception ex) : base(message, ex)
    {
    }

    protected PaylocityBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
