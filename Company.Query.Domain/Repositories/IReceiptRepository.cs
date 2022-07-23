using Company.Query.Domain.Abstractions.Entities;
using System;
using System.Threading.Tasks;

namespace Company.Query.Domain.Repositories
{
    public interface IReceiptRepository
    {
        Task<PaymentReceived> GetReceipt(string endToEndId);

        Task<PaymentReceived> GetPaymentReceivedReceipt(Guid checkingaccount, Guid transactionId);

        Task<PaymentDone> GetPaymentDoneReceipt(Guid checkingaccount, Guid transactionId);
    }
}
