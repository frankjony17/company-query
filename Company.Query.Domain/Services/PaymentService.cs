using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Acls.DailyEntry;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Exceptions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly Actions _actions;

        private readonly IPaymentRepository _paymentRepository;
        private readonly IDailyEntryAcl _dailyEntryAcl;
        private readonly ILogger<PaymentService> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public PaymentService(IPaymentRepository paymentRepository
            , IDailyEntryAcl dailyEntryAcl
            , Actions actions
            , ILogger<PaymentService> logger
            , RequestContextHolder requestContextHolder)
        {
            _dailyEntryAcl = dailyEntryAcl;
            _paymentRepository = paymentRepository;
            _actions = actions;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        public async Task<PaymentCompleteResponse> GetPaymentStatus(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Searching for payment with E2E ID = {endToEndId}");

            var response = await _paymentRepository.GetPaymentStatus(endToEndId);
            
            if (response != null)
            {
                _logger.LogInformation($"Payment status for end to end id {endToEndId} is {response.Status}.");

                var transactionLegacyId = await _dailyEntryAcl.GetLegacyIdByTransactionUuid(response.TransactionId);

                response.SetLegacyTransactionId(transactionLegacyId.TransactionId);

            } else
            {
                _logger.LogWarning($"Payment not found for end to end id {endToEndId}");
            }

            var createResponse = new PaymentCompleteResponse(endToEndId, response, _actions);

            return createResponse;
        }

        public async Task<CompanyTransactionsCompleteResponse> GetCompanyTransactions(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Searching for transactions with E2E ID = {endToEndId}");

            var companyTransactionsCompleteResponse = CompanyTransactionsCompleteResponse.Create(endToEndId, _actions);

            var companyTransactionsResponse = await _paymentRepository.GetCompanyTransactions(endToEndId);

            if (!companyTransactionsResponse.Any())
            {
                _logger.LogInformation($"No transactions found for end to end id {endToEndId}.");
                
                throw new NotFoundCustomException("Not found", "No transactions found for refund.");
            }

            _logger.LogInformation($"{companyTransactionsResponse.Count()} transaction(s) found in company DB for end to end id {endToEndId}.");

            companyTransactionsCompleteResponse.CreateTransctions();

            companyTransactionsCompleteResponse.CompanyTransactions.SetAllowRefund(GetAllowRefund(companyTransactionsResponse));

            var dailyEntryTransactions = await _dailyEntryAcl.GetDailyEntryByTransactions(GetTransactionsUuid(companyTransactionsResponse));

            if(!dailyEntryTransactions.Any())
            {
                _logger.LogInformation($"No transactions found in daily entry for end to end id {endToEndId}.");

                return CompanyTransactionsCompleteResponse.Create(endToEndId, _actions);
            }

            _logger.LogInformation($"{dailyEntryTransactions.Count()} transaction(s) found in daily entry for end to end id {endToEndId}.");

            companyTransactionsCompleteResponse.CompanyTransactions.AddTransactionsList(dailyEntryTransactions);

            companyTransactionsCompleteResponse.CompanyTransactions.SetSourceTransaction(GetTransactionSource(companyTransactionsResponse));

            companyTransactionsCompleteResponse.CompanyTransactions.SetReceiptDate(GetCompanyReceiptDate(companyTransactionsResponse));

            companyTransactionsCompleteResponse.CompanyTransactions.CalculateRemainingValue();

            companyTransactionsCompleteResponse.CompanyTransactions.CalculateRemainingDays();

            return companyTransactionsCompleteResponse;
        }

        private DateTime GetCompanyReceiptDate(IEnumerable<CompanyTransactionsDbResponse> companyTransactionsResponse)
        {
            var companyReceipt = companyTransactionsResponse.FirstOrDefault(s => s.IsSource);

            return companyReceipt.CompanyDate;
        }

        bool GetAllowRefund(IEnumerable<CompanyTransactionsDbResponse> companyTransactionsResponse)
        {
            return companyTransactionsResponse.Any(filter => filter.IsSource == filter.AllowRefund);
        }

        IEnumerable<Guid> GetTransactionsUuid(IEnumerable<CompanyTransactionsDbResponse> companyTransactionsResponse)
        {
            return companyTransactionsResponse.Select(s => s.TransactionUuid);
        }

        public Guid GetTransactionSource(IEnumerable<CompanyTransactionsDbResponse> companyTransactionsResponse)
        {
            return companyTransactionsResponse.FirstOrDefault(filter => filter.IsSource).TransactionUuid;
        }
    }
}
