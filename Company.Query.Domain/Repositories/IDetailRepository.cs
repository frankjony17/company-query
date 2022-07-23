using Company.Query.Domain.Providers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Query.Domain.Repositories
{
    public interface IDetailRepository
    {
        Task<CompanyDetailResponse> Get(string endToEndId);
        Task<CompanyDetailPaymentResponse> GetPayment(string endToEndId);
        Task<IEnumerable<CompanyDetailPaymentEventResponse>> GetPaymentEvents(string endToEndId);
        Task<IEnumerable<CompanyDetailRefundEventResponse>> GetRefundEvents(string endToEndId);
        Task<IEnumerable<CompanyDetailReceiptResponse>> GetReceipts(string endToEndId);
    }
}
