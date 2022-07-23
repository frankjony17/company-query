using System;
using Company.Query.Domain.Abstractions.Attributes;
using Company.Query.Domain.Abstractions.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Company.Query.Domain.Abstractions.Entities
{
    [BsonCollection("COMPANY_RECEIPT")]
    public class PaymentReceived : ICompanyEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired()]
        [BsonElement("END_TO_END_ID")]
        public string EndToEndId { get; set; }

        [BsonElement("RECEIPT_KEY_ID")]
        public string ReceiptKeyId { get; set; }

        [BsonElement("KEY_TYPE_DS")]
        public string KeyTypeDescription { get; set; }

        [BsonElement("PARTNER_ISPB_DS")]
        public string PartnerIspbName { get; set; }

        [BsonRequired()]
        [BsonElement("SENDER_NM")]
        public string SenderName { get; set; }

        [BsonElement("SENDER_KEY_ID")]
        public string SenderKeyId { get; set; }

        [BsonElement("SENDER_KEY_BRANCH_NU")]
        public string SenderKeyBranchNumber { get; set; }

        [BsonElement("SENDER_KEY_CHECKING_ACCOUNT_NU")]
        public string SenderKeyCheckingAccountNumber { get; set; }

        [BsonElement("SENDER_CPF_CNPJ_NU")]
        public string SenderDocument { get; set; }

        [BsonElement("COMPANY_CM")]
        public string Comment { get; set; }

        [BsonRequired()]
        [BsonElement("COMPANY_VL")]
        public double Value { get; set; }

        [BsonRequired()]
        [BsonElement("COMPANY_DT")]
        public DateTime Date { get; set; }

        [BsonRequired()]
        [BsonElement("TRANSACTIONS_UUID")]
        public string TransactionId { get; set; }

        [BsonElement("RETURNE_ID")]
        public string ReturnId { get; set; }

        [BsonRequired()]
        [BsonElement("CHECKING_ACCOUNT_UUID")]
        public string CheckingAccount { get; set; }

        [BsonElement("BENEFICIARY_KEY_BRANCH_NU")]
        public string BeneficiaryKeyBranchNumber { get; set; }

        [BsonElement("BENEFICIARY_KEY_CHECKING_ACCOUNT_NU")]
        public string BeneficiaryKeyCheckingAccountNumber { get; set; }

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
