using Company.Query.Infra.CrossCutting.Interfaces.Exception;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Company.Query.Api.Infra.Exceptions
{
    [Serializable]
    public class LoggedDocumentNotFoundException : Exception, ICustomException
    {
        private const string TITLE = "Unable to identify logged user.";

        public LoggedDocumentNotFoundException() : base("'document' header not found.")
        {
        }

        public LoggedDocumentNotFoundException(string message) : base(message)
        {
        }

        public LoggedDocumentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoggedDocumentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Title => TITLE;

        public int StatusCode => (int)HttpStatusCode.BadRequest;
    }
}
