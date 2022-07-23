using System;
using Company.Query.Domain.Abstractions.Attributes;
using Company.Query.Domain.Abstractions.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Company.Query.Domain.Abstractions.Entities
{
    [BsonCollection("COMPANY")]
    public class PaymentDone : ICompanyEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired()]
        [BsonElement("END_TO_END_ID")]
        public string EndToEndId { get; set; }

        [BsonElement("PARTNER_ISPB_DS")]
        public string PartnerIspbName { get; set; }

        [BsonElement("BENEFICIARY_KEY_ID")]
        public string BeneficiaryKeyId { get; set; }

        [BsonRequired()]
        [BsonElement("BENEFICIARY_NM")]
        public string BeneficiaryName { get; set; }

        [BsonElement("BENEFICIARY_KEY_BRANCH_NU")]
        public string BeneficiaryKeyBranchNumber { get; set; }

        [BsonElement("BENEFICIARY_KEY_CHECKING_ACCOUNT_NU")]
        public string BeneficiaryKeyCheckingAccountNumber { get; set; }

        [BsonElement("BENEFICIARY_CPF_CNPJ_NU")]
        public string BeneficiaryDocument { get; set; }

        [BsonElement("SENDER_KEY_ID")]
        public string SenderKeyId { get; set; }

        [BsonElement("SENDER_KEY_TYPE_DS")]
        public string SenderKeyType { get; set; }

        [BsonElement("SENDER_KEY_BRANCH_NU")]
        public string SenderKeyBranchNumber { get; set; }

        [BsonElement("SENDER_KEY_CHECKING_ACCOUNT_NU")]
        public string SenderKeyCheckingAccountNumber { get; set; }

        [BsonElement("PAYER_REQUEST_CM")]
        public string PayerRequest { get; set; }

        [BsonElement("COMPANY_CM")]
        public string Comment { get; set; }

        [BsonRequired()]
        [BsonElement("COMPANY_VL")]
        public double Value { get; set; }

        [BsonRequired()]
        [BsonElement("COMPANY_DT")]
        public DateTime Date { get; set; }

        [BsonElement("COMPANY_TYPE_DS")]
        public string PaymentType { get; set; }

        [BsonElement("COMPANY_REVERSAL_ID")]
        public int PaymentReversalId { get; set; }

        [BsonElement("COMPANY_REVERSAL_DS")]
        public string PaymentReversal { get; set; }

        [BsonRequired()]
        [BsonElement("TRANSACTIONS_UUID")]
        public string TransactionId { get; set; }

        [BsonElement("RETURNE_ID")]
        public string ReturnId { get; set; }

        [BsonRequired()]
        [BsonElement("CHECKING_ACCOUNT_UUID")]
        public string CheckingAccount { get; set; }

        [BsonElement("QRCODE_NM")]
        public string QrCodeName { get; set; }

        [BsonElement("QRCODE_ORIGINAL_VL")]
        public double? QrCodeValue { get; set; }

        [BsonElement("QRCODE_DUE_DT")]
        public DateTime? QrCodeDueDate { get; set; }

        [BsonElement("QRCODE_INTEREST_VL")]
        public double? QrCodeInterestValue { get; set; }

        [BsonElement("QRCODE_FINE_VL")]
        public double? QrCodeFineValue { get; set; }

        [BsonElement("QRCODE_REDUCTION_VL")]
        public double? QrCodeReductionValue { get; set; }

        [BsonElement("QRCODE_DISCOUNT_VL")]
        public double? QrCodeDiscountValue { get; set; }

        [BsonRequired()]
        [BsonElement("CREATED_AT_DT")]
        public DateTime CreatedAt { get; set; }

        [BsonRequired()]
        [BsonElement("CREATED_BY_DS")]
        public string CreatedBy { get; set; }

        [BsonElement("REFUND")]
        public CompanyRefund Refund { get; set; }

        [BsonElement("BROKER_ID")]
        public int BrokerId { get; set; }
    }
}
