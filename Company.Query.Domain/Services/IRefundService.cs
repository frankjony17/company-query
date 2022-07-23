using Company.Query.Domain.Providers.Responses;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public interface IRefundService
    {
        Task<RefundCompleteResponse> GetRefundStatus(string returnId);
    }
}
