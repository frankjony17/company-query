using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public class DetailService : IDetailService
    {
        private readonly IDetailRepository _detailRepository;
        private readonly ILogger<DetailService> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public DetailService(IDetailRepository detailRepository
            , ILogger<DetailService> logger
            , RequestContextHolder requestContextHolder)
        {
            _detailRepository = detailRepository;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        public async Task<CompanyDetailResponse> Get(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Searching for company with EndToEndId = [{endToEndId}]");
            var response = await _detailRepository.Get(endToEndId);

            if (response != null)
            {
                _logger.LogInformation($"Searching for payment with EndToEndId = [{endToEndId}]");
                var payments = await _detailRepository.GetPayment(endToEndId);
                if (payments != null)
                    response.SetCompanyDetailPaymentResponse(payments);

                _logger.LogInformation($"Searching for payment events with EndToEndId = [{endToEndId}]");
                var paymentEvents = await _detailRepository.GetPaymentEvents(endToEndId);
                if (paymentEvents.Any())
                    response.CompanyDetailPayment.SetCompanyDetailPaymentEventResponse(paymentEvents);

                _logger.LogInformation($"Searching for refund events with EndToEndId = [{endToEndId}]");
                var refundEvents = await _detailRepository.GetRefundEvents(endToEndId);
                if (refundEvents.Any())
                {
                    response.SetCompanyDetailRefundResponse(new CompanyDetailRefundResponse());
                    response.CompanyDetailRefund.SetCompanyDetailRefundResponse(refundEvents);
                }

                _logger.LogInformation($"Searching for receipts with EndToEndId = [{endToEndId}]");
                var receipts = await _detailRepository.GetReceipts(endToEndId);
                if (receipts.Any())
                    response.SetCompanyDetailReceiptResponse(receipts);
            }
            else
            {
                _logger.LogWarning($"Company not found for EndToEndId =[{endToEndId}]");
            }

            return response;

        }

    }
}
