using Company.Query.Infra.CrossCutting.Interfaces.Exception;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Company.Query.Domain.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class NotFoundCustomException : Exception, ICustomException
    {
        public string Title { get; }

        public int StatusCode => (int)HttpStatusCode.NotFound;

        public NotFoundCustomException(string title, string message)
             : base(message)
        {
            Title = title;
        }

        public NotFoundCustomException()
        {
        }

        protected NotFoundCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundCustomException(string message) : base(message)
        {
        }

        public NotFoundCustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
