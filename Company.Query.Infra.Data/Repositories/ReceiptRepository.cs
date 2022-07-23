using Company.Query.Domain.Repositories;
using System.Threading.Tasks;
using Company.Query.Domain.Abstractions.Entities;
using Company.Query.Infra.Data.Contexts;
using System;

namespace Company.Query.Infra.Data.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMongoDBRepository<PaymentReceived> _paymentReceivedRepository;
        private readonly IMongoDBRepository<PaymentDone> _paymentDoneRepository;

        public ReceiptRepository(
            IUnitOfWork unitOfWork, 
            IMongoDBRepository<PaymentReceived> paymentReceivedRepository,
            IMongoDBRepository<PaymentDone> paymentDoneRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentReceivedRepository = paymentReceivedRepository;
            _paymentDoneRepository = paymentDoneRepository;
        }

        public async Task<PaymentReceived> GetReceipt(string endToEndId)
        {
            return await _paymentReceivedRepository.FindByEndtoendAsync(endToEndId);
        }

        public async Task<PaymentReceived> GetPaymentReceivedReceipt(Guid checkingaccount, Guid transactionId)
        {
            return await _paymentReceivedRepository.FindOneAsync(
                x => x.CheckingAccount == checkingaccount.ToString() && x.TransactionId == transactionId.ToString());
        }

        public async Task<PaymentDone> GetPaymentDoneReceipt(Guid checkingaccount, Guid transactionId)
        {
            return await _paymentDoneRepository.FindOneAsync(
                x => x.CheckingAccount == checkingaccount.ToString() && x.TransactionId == transactionId.ToString());
        }
    }
}
