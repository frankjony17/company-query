using Company.JsonApiSpec.Attributes;
using System;
using System.Collections.Generic;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyDetailResponse
    {
        [Id]
        public string EndToEndId { get; set; }
        [Attr]
        public Guid CheckingAccountUuid { get; set; }
        [Attr]
        public CompanyDetailPaymentResponse CompanyDetailPayment { get; set; }
        [Attr]
        public CompanyDetailRefundResponse CompanyDetailRefund { get; set; }
        [Attr]
        public IEnumerable<CompanyDetailReceiptResponse> CompanyDetailReceipt { get; set; }

        public void SetCompanyDetailPaymentResponse(CompanyDetailPaymentResponse companyDetailPayment)
        {
            CompanyDetailPayment = companyDetailPayment;
        }
        public void SetCompanyDetailRefundResponse(CompanyDetailRefundResponse companyDetailRefund)
        {
            CompanyDetailRefund = companyDetailRefund;
        }
        public void SetCompanyDetailReceiptResponse(IEnumerable<CompanyDetailReceiptResponse> companyDetailReceipt)
        {
            CompanyDetailReceipt = companyDetailReceipt;
        }
    }
}
