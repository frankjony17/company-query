using Company.Query.Domain.Repositories;
using System.Threading.Tasks;
using Company.Query.Infra.Data.Contexts;
using Company.Query.Infra.Data.Repositories.SQLs;
using Company.Query.Domain.Providers.Responses;
using System.Collections.Generic;
using Dapper;

namespace Company.Query.Infra.Data.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        protected readonly IUnitOfWork _unitOfWork;

        public DetailRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<CompanyDetailResponse> Get(string endToEndId)
        {
            return await _unitOfWork.QueryFirstOrDefaultAsync<CompanyDetailResponse>(DetailSql.SelectCompany, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }

        public async Task<CompanyDetailPaymentResponse> GetPayment(string endToEndId)
        {
            return await _unitOfWork.QueryFirstOrDefaultAsync<CompanyDetailPaymentResponse>(DetailSql.SelectPayment, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }

        public async Task<IEnumerable<CompanyDetailPaymentEventResponse>> GetPaymentEvents(string endToEndId)
        {
            return await _unitOfWork.QueryAsync<CompanyDetailPaymentEventResponse>(DetailSql.SelectPaymentEvents, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }

        public async Task<IEnumerable<CompanyDetailRefundEventResponse>> GetRefundEvents(string endToEndId)
        {
            return await _unitOfWork.QueryAsync<CompanyDetailRefundEventResponse>(DetailSql.SelectRefundEvents, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }

        public async Task<IEnumerable<CompanyDetailReceiptResponse>> GetReceipts(string endToEndId)
        {
            return await _unitOfWork.QueryAsync<CompanyDetailReceiptResponse>(DetailSql.SelectReceipt, new
            {
                EndToEnd = new DbString { Value = endToEndId, IsFixedLength = false, IsAnsi = true, Length = 50 }
            });
        }
    }
}
