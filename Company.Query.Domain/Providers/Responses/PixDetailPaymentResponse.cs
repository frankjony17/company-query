using Company.JsonApiSpec.Attributes;
using System.Collections.Generic;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyDetailPaymentResponse
    {
        [Attr]
        public string BeneficiaryName { get; set; }
        [Attr]
        public string BeneficiaryKeyId { get; set; }
        [Attr]
        public string BeneficiaryBranch { get; set; }
        [Attr]
        public string BeneficiaryCheckingAccount { get; set; }
        [Attr]
        public string BeneficiaryCpfCnpj { get; set; }
        [Attr]
        public int BeneficiaryKeyTypeId { get; set; }
        [Attr]
        public string SameOwnership { get; set; }
        [Attr]
        public int? Bank { get; set; }
        [Attr]
        public int? IspbPrincipal { get; set; }
        [Attr]
        public int Ispb { get; set; }
        [Attr]
        public string IspnName { get; set; }
        [Attr]
        public int KeyTypeId { get; set; }
        [Attr]
        public string KeyTypeDescription { get; set; }
        [Attr]
        public int PaymentTypeId { get; set; }
        [Attr]
        public string PaymentTypeDescription { get; set; }
        [Attr]
        public IEnumerable<CompanyDetailPaymentEventResponse> PaymentEvents { get; set; }

        public void SetCompanyDetailPaymentEventResponse(IEnumerable<CompanyDetailPaymentEventResponse> paymentEvents)
        {
            PaymentEvents = paymentEvents;
        }
    }
}
