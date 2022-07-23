using Company.JsonApiSpec.Attributes;
using Company.Query.Domain.Abstractions.Entities;
using System;

namespace Company.Query.Api.Responses
{
    public class PaymentReceivedResponse
    {
        [Id]
        public Guid Id => Guid.NewGuid();

        [Attr]
        public string EndToEndId { get; set; }

        [Attr]
        public string ReceiptKeyId { get; set; }

        [Attr]
        public string KeyTypeDescription { get; set; }

        [Attr]
        public string PartnerIspbName { get; set; }

        [Attr]
        public string SenderName { get; set; }

        [Attr]
        public string SenderKeyId { get; set; }

        [Attr]
        public string SenderKeyBranchNumber { get; set; }

        [Attr]
        public string SenderKeyCheckingAccountNumber { get; set; }

        [Attr]
        public string SenderDocument { get; set; }

        [Attr]
        public string Comment { get; set; }

        [Attr]
        public double Value { get; set; }

        [Attr]
        public DateTime Date { get; set; }

        [Attr]
        public Guid TransactionId { get; set; }

        [Attr]
        public string ReturnId { get; set; }

        [Attr]
        public Guid CheckingAccount { get; set; }

        [Attr]
        public string BeneficiaryKeyBranchNumber { get; set; }

        [Attr]
        public string BeneficiaryKeyCheckingAccountNumber { get; set; }

        [Attr]
        public DateTime CreatedAt { get; set; }

        [Attr]
        public string CreatedBy { get; set; }
        
        [Attr]
        public CompanyRefund Refund { get; set; }

        [Attr]
        public int BrokerId { get; set; }
    }
}
