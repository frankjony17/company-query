using Company.JsonApiSpec.Attributes;
using System.Collections.Generic;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyDetailRefundResponse
    {
        [Attr]
        public IEnumerable<CompanyDetailRefundEventResponse> DetailRefundEvent { get; set; }

        public void SetCompanyDetailRefundResponse(IEnumerable<CompanyDetailRefundEventResponse> detailRefundEvent)
        {
            DetailRefundEvent = detailRefundEvent;
        }
    }
}
