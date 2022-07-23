using System;

namespace Company.Query.Domain.Abstractions.Interfaces
{
    public interface ICompanyPayment : ICompanyEntity
    {
        string Id { get; set; }

        Guid CheckingAccountId { get; set; }

        Guid TransactionsId { get; set; }

    }
}