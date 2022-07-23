using Company.JsonApiSpec.Attributes;
using Company.Query.Domain.Configurations;

namespace Company.Query.Domain.Providers.Responses
{
    public class PaymentCompleteResponse
    {
        [Id]
        public string Id { get; set; }
        [Attr]
        public PaymentStatusResponse Payment { get; set; }
        [Attr]
        public Actions Actions { get; set; }

        public PaymentCompleteResponse(string endToEndId, PaymentStatusResponse response, Actions actions)
        {
            Id = endToEndId;
            Payment = response;
            Actions = actions;
        }
    }    
}
