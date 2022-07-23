using Company.Query.Domain.Repositories;
using System.Threading.Tasks;
using Company.Query.Infra.Data.Contexts;
using Company.Query.Infra.Data.Repositories.SQLs;
using Company.Query.Domain.Providers.Responses;
using Dapper;

namespace Company.Query.Infra.Data.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        protected readonly IUnitOfWork _unitOfWork;

        public RefundRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<RefundStatusResponse> GetRefundStatus(string returnId)
        {
            var payment = await _unitOfWork.QueryFirstOrDefaultAsync<RefundStatusResponse>(RefundSQL.GET_REFUND_STATUS, new
            {
                ReturnId = new DbString { Value = returnId, IsFixedLength = false, IsAnsi = true, Length = 32 } 
            });

            return payment;
        }
    }
}
