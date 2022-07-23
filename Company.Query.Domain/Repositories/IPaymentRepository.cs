using Company.Query.Domain.Abstractions.Entities;
using Company.Query.Domain.Providers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Query.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<PaymentStatusResponse> GetPaymentStatus(string endToEndId);
        Task<IEnumerable<CompanyTransactionsDbResponse>> GetCompanyTransactions(string endToEndId);
    }
}
