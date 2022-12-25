using System.Runtime.Serialization;

namespace Web.Infrastructure.Exceptions;

public class ConfigurationException : Exception
{
    protected ConfigurationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ConfigurationException(string? message)
        : base(message)
    {
    }

    public ConfigurationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}