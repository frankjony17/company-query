using Company.JsonApiSpec.Attributes;
using Company.Query.Domain.Abstractions.Entities;
using System;

namespace Company.Query.Api.Responses
{
    public class PaymentDoneResponse
    {
        [Id]
        public Guid Id => Guid.NewGuid();

        [Attr]
        public string EndToEndId { get; set; }

        [Attr]
        public string PartnerIspbName { get; set; }

        [Attr]
        public string BeneficiaryKeyId { get; set; }

        [Attr]
        public string BeneficiaryName { get; set; }

        [Attr]
        public string BeneficiaryKeyBranchNumber { get; set; }

        [Attr]
        public string BeneficiaryKeyCheckingAccountNumber { get; set; }

        [Attr]
        public string BeneficiaryDocument { get; set; }

        [Attr]
        public string SenderKeyId { get; set; }

        [Attr]
        public string SenderKeyType { get; set; }

        [Attr]
        public string SenderKeyBranchNumber { get; set; }

        [Attr]
        public string SenderKeyCheckingAccountNumber { get; set; }

        [Attr]
        public string PayerRequest { get; set; }

        [Attr]
        public string Comment { get; set; }

        [Attr]
        public double Value { get; set; }

        [Attr]
        public DateTime Date { get; set; }

        [Attr]
        public string PaymentType { get; set; }

        [Attr]
        public int PaymentReversalId { get; set; }

        [Attr]
        public string PaymentReversal { get; set; }

        [Attr]
        public Guid TransactionId { get; set; }

        [Attr]
        public string ReturnId { get; set; }

        [Attr]
        public Guid CheckingAccount { get; set; }

        [Attr]
        public string QrCodeName { get; set; }

        [Attr]
        public double? QrCodeValue { get; set; }

        [Attr]
        public DateTime? QrCodeDueDate { get; set; }

        [Attr]
        public double? QrCodeInterestValue { get; set; }

        [Attr]
        public double? QrCodeFineValue { get; set; }

        [Attr]
        public double? QrCodeReductionValue { get; set; }

        [Attr]
        public double? QrCodeDiscountValue { get; set; }

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
