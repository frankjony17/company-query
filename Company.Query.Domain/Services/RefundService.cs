using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Acls.DailyEntry;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        private readonly IDailyEntryAcl _dailyEntryAcl;
        private readonly Actions _actions;
        private readonly ILogger<RefundService> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public RefundService(IRefundRepository refundRepository, Actions actions, IDailyEntryAcl dailyEntryAcl, ILogger<RefundService> logger, RequestContextHolder requestContextHolder)
        {
            _refundRepository = refundRepository;
            _actions = actions;
            _dailyEntryAcl = dailyEntryAcl;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        public async Task<RefundCompleteResponse> GetRefundStatus(string returnId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);

            _logger.LogInformation($"Searching refund with returnId = {returnId}");

            var response = await _refundRepository.GetRefundStatus(returnId);

            if (response != null)
            {
                _logger.LogInformation($"Refund found for return id {returnId} with status {response.Status}.");

                var transactionLegacyId = await _dailyEntryAcl.GetLegacyIdByTransactionUuid(response.TransactionId);
                response.SetLegacyTransactionId(transactionLegacyId.TransactionId);
            }
            else
            {
                _logger.LogWarning($"Refund NOT found for return id {returnId}.");
            }

            var createResponse = new RefundCompleteResponse(returnId, response, _actions);

            return createResponse;
        }
    }
}
