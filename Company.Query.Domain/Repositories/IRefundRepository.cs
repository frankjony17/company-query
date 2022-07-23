using Company.Query.Domain.Providers.Responses;
using System.Threading.Tasks;

namespace Company.Query.Domain.Repositories
{
    public interface IRefundRepository
    {
        Task<RefundStatusResponse> GetRefundStatus(string returnId);
    }
}
