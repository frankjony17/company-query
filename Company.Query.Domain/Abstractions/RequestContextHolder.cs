using System;

namespace Company.Query.Domain.Abstractions
{
    public class RequestContextHolder
    {
        public RequestContextHolder()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; private set; }
        public string ContextName { get; set; }
    }
}
