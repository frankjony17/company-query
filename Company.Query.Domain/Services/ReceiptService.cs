using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Abstractions.Entities;
using Company.Query.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly ILogger<ReceiptService> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public ReceiptService(IReceiptRepository receiptRepository, ILogger<ReceiptService> logger, RequestContextHolder requestContextHolder)
        {
            _receiptRepository = receiptRepository;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        public async Task<PaymentReceived> GetPaymentReceivedReceipt(Guid checkingaccount, Guid transactionId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Querying for payment received receipt in MongoDB. CheckingAccountUuid = {checkingaccount} - TransactionUuid = {transactionId}");

            return await _receiptRepository.GetPaymentReceivedReceipt(checkingaccount, transactionId);
        }

        public async Task<PaymentDone> GetPaymentDoneReceipt(Guid checkingaccount, Guid transactionId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogInformation($"Querying for payment done receipt in MongoDB. CheckingAccountUuid = {checkingaccount} - TransactionUuid = {transactionId}");

            return await _receiptRepository.GetPaymentDoneReceipt(checkingaccount, transactionId);
        }
    }
}
