using Newtonsoft.Json;
using Company.Query.Domain.Enums;
using System;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyTransactionsAssociatedListResponse
    {
        public int TransactionId { get; private set; }
        public Guid TransactionUuid { get; private set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public string TransactionDate { get; private set; }
        public int CategoryId { get; private set; }
        public bool IsSource { get; private set; }
        [JsonIgnore]
        public TipoOperacao TypeOperation { get; set; }

        private CompanyTransactionsAssociatedListResponse(int transactionId, Guid transactionUuid, decimal value, string description, DateTime transactionDate, int categoryId, TipoOperacao typeOperation)
        {
            TransactionId = transactionId;
            TransactionUuid = transactionUuid;
            TypeOperation = typeOperation;

            if (TypeOperation == TipoOperacao.Debito)
            {
                Value = Decimal.Negate(value);
            }
            else
            {
                Value = value;
            }

            Description = description;
            TransactionDate = transactionDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff");
            CategoryId = categoryId;
        }

        public static CompanyTransactionsAssociatedListResponse Create(int transactionId, Guid transactionUuid, decimal value, string description, DateTime transactionDate, int categoryId, TipoOperacao typeOperation)
            => new CompanyTransactionsAssociatedListResponse(transactionId, transactionUuid, value, description, transactionDate, categoryId, typeOperation);

        public void SetIsSource() => IsSource = true;
    }
}
