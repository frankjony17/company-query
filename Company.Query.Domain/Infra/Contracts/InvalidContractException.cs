using Flunt.Validations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Company.Query.Domain.Infra.Contracts
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidContractException : Exception
    {
        public Contract Contract { get; private set; }

        public InvalidContractException(Contract contract) : base(string.Join(", ", contract.Notifications.Select(o => o.Message).ToArray()))
        {
            Contract = contract;
        }

        protected InvalidContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
