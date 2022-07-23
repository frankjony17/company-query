using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class RefundStatusResponse
    {
        public string ReturnId { get; set; }
        public int Status { get; set; }
        public Guid TransactionId { get; set; }
        public int LegacyTransactionId { get; set; }

        public void SetLegacyTransactionId(int legacyTransactionId)
        {
            LegacyTransactionId = legacyTransactionId;
        }
    }
}
