using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyTransactionsDbResponse
    {
        public Guid TransactionUuid { get; set; }
        public bool IsSource { get; set; }
        public bool AllowRefund { get; set; }
        public DateTime CompanyDate { get; set; }
    }
}
