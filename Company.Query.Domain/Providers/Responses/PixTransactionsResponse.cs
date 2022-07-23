using Company.Query.Domain.Providers.Acls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyTransactionsResponse
    {
        public decimal RemainingValue { get; private set; }
        public int RemainingDays { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public bool AllowRefund { get; private set; }
        public List<CompanyTransactionsAssociatedListResponse> AssociatedList { get; set; }

        private CompanyTransactionsResponse() => AssociatedList = new List<CompanyTransactionsAssociatedListResponse>();

        public static CompanyTransactionsResponse Create() => new CompanyTransactionsResponse();

        public void SetAllowRefund(bool allowRefund) => AllowRefund = allowRefund;

        public void AddTransactionsList(IEnumerable<DailyEntryResponseDto> dailyEntryResponseDtos)
        {
            AssociatedList =
                dailyEntryResponseDtos
                    .Select(
                    s =>
                        CompanyTransactionsAssociatedListResponse.Create(s.TransactionId, s.TransactionUuid, s.Value, s.Description, s.TransactionDate, s.CategoryId, s.TypeOperation)
                    ).OrderByDescending(t => t.TransactionDate).ToList();
        }

        public void SetSourceTransaction(Guid transactionUuid)
        {
            var sourceTransaction = AssociatedList.FirstOrDefault(filter => filter.TransactionUuid == transactionUuid);

            sourceTransaction?.SetIsSource();

            if (sourceTransaction?.TypeOperation == Enums.TipoOperacao.Debito)
            {
                SetAllowRefund(false);
            }
        }

        public void SetReceiptDate(DateTime receiptDate)
        {
            ReceiptDate = receiptDate;
        }

        public void CalculateRemainingDays()
        {
            var date = ReceiptDate.AddDays(90);

            var remainingDays = (int)date.Subtract(DateTime.Now).TotalDays;

            RemainingDays = Math.Sign(remainingDays) == -1 ? 0 : remainingDays;
        }

        public void CalculateRemainingValue()
        {
            RemainingValue = AssociatedList.Sum(s => s.Value);

            if (RemainingValue <= 0)
            {
                SetAllowRefund(false);
            }
        }
    }
}
