using Company.Query.Infra.CrossCutting.Interfaces.Exception;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Company.Query.Domain.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class BadRequestCustomException : Exception, ICustomException
    {
        public string Title { get; }

        public int StatusCode => (int)HttpStatusCode.BadRequest;

        public BadRequestCustomException(string title, string message)
             : base(message)
        {
            Title = title;
        }

        public BadRequestCustomException()
        {
        }

        protected BadRequestCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BadRequestCustomException(string message) : base(message)
        {
        }

        public BadRequestCustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
