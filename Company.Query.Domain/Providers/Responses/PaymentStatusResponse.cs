using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class PaymentStatusResponse
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public Guid TransactionId { get; set; }
        public int LegacyTransactionId { get; set; }
        public int StatusErrorId { get; set; }

        public void SetLegacyTransactionId(int legacyTransactionId)
        {
            LegacyTransactionId = legacyTransactionId;
        }
    }
}
