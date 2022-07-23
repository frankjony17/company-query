using Company.Query.Domain.Abstractions.Entities;
using System;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public interface IReceiptService
    {
        Task<PaymentReceived> GetPaymentReceivedReceipt(Guid checkingaccount, Guid transactionId);

        Task<PaymentDone> GetPaymentDoneReceipt(Guid checkingaccount, Guid transactionId);
    }
}
