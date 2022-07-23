using Company.Query.Domain.Repositories;
using System.Threading.Tasks;
using Company.Query.Infra.Data.Contexts;
using Company.Query.Infra.Data.Repositories.SQLs;
using Company.Query.Domain.Providers.Responses;
using System.Collections.Generic;
using Dapper;

namespace Company.Query.Infra.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        protected readonly IUnitOfWork _unitOfWork;

        public PaymentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<PaymentStatusResponse> GetPaymentStatus(string endToEndId)
        {
            return await _unitOfWork.QueryFirstOrDefaultAsync<PaymentStatusResponse>(PaymentSQL.GET_PAYMENT_STATUS, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }

        public async Task<IEnumerable<CompanyTransactionsDbResponse>> GetCompanyTransactions(string endToEndId)
        {
            return await _unitOfWork.QueryAsync<CompanyTransactionsDbResponse>(PaymentSQL.GET_COMPANY_TRANSACTIONS, new
            {
                EndToEndId = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }
    }
}
