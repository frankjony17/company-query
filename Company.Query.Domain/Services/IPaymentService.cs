using Company.Query.Domain.Providers.Responses;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public interface IPaymentService
    {
        Task<PaymentCompleteResponse> GetPaymentStatus(string endToEndId);
        Task<CompanyTransactionsCompleteResponse> GetCompanyTransactions(string endToEndId);
    }
}
