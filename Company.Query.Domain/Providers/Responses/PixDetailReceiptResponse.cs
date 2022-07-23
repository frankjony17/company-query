using Company.JsonApiSpec.Attributes;
using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyDetailReceiptResponse
    {
        [Attr]
        public int Version { get; set; }
        [Attr]
        public string SenderName { get; set; }
        [Attr]
        public int SenderPartnerISPBNu { get; set; }
        [Attr]
        public string SenderKeyBranchNu { get; set; }
        [Attr]
        public string SenderCpfCnpj { get; set; }
        [Attr]
        public double Value { get; set; }
        [Attr]
        public DateTime Date { get; set; }
        [Attr]
        public string Commentary { get; set; }
        [Attr]
        public Guid? TransactionsUuid { get; set; }
        [Attr]
        public string ReturneId { get; set; }
        [Attr]
        public DateTime CreatedAt { get; set; }

    }
}
