using Company.JsonApiSpec.Attributes;
using Company.Query.Domain.Configurations;

namespace Company.Query.Domain.Providers.Responses
{
    public class RefundCompleteResponse
    {
        [Id]
        public string Id { get; set; }
        [Attr]
        public RefundStatusResponse Refund { get; set; }
        [Attr]
        public Actions Actions { get; set; }

        public RefundCompleteResponse()
        {

        }

        public RefundCompleteResponse(string returnId, RefundStatusResponse response, Actions actions)
        {
            Id = returnId;
            Refund = response;
            Actions = actions;
        }
    }    
}
