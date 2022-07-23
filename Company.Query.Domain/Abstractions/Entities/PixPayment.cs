using Company.Query.Domain.Abstractions.Interfaces;
using Company.Query.Domain.Abstractions.Attributes;
using System;

namespace Company.Query.Domain.Abstractions.Entities
{
    [BsonCollection("COMPANY")]
    public class CompanyPayment : ICompanyPayment
    {
        public string Id { get; set; }

        public Guid CheckingAccountId { get; set; }

        public Guid TransactionsId { get; set; }

        public string EndToEndId { get; set; }
    }
}