
using Company.JsonApiSpec.Attributes;
using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyDetailPaymentEventResponse
    {
        [Attr]
        public DateTime CreatedAt { get; set; }
        [Attr]
        public int Status { get; set; }
        [Attr]
        public string StatusDescription { get; set; }
        [Attr]
        public int StatusError { get; set; }
        [Attr]
        public string StatusErrorDescription { get; set; }
        [Attr]
        public int PaymentEventNu { get; set; }
        [Attr]
        public DateTime TransactionsDate { get; set; }
        [Attr]
        public int TranscationsType { get; set; }
        [Attr]
        public Guid? TransactionsUuid { get; set; }
        [Attr]
        public double Value { get; set; }
        [Attr]
        public string BrokerId { get; set; }
    }
}
