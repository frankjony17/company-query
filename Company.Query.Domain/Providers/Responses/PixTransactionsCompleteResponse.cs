using Company.JsonApiSpec.Attributes;
using Company.Query.Domain.Configurations;

namespace Company.Query.Domain.Providers.Responses
{
    public class CompanyTransactionsCompleteResponse
    {
        [Id]
        public string Id { get; private set; }
        [Attr]
        public CompanyTransactionsResponse CompanyTransactions { get; private set; }
        [Attr]
        public Actions Actions { get; private set; }

        private CompanyTransactionsCompleteResponse(string endToEndId, Actions actions)
        {
            Id = endToEndId;
            //DailyEntryTransactions = response;
            Actions = actions;
        }

        public static CompanyTransactionsCompleteResponse Create(string endToEndId, Actions actions)
            => new CompanyTransactionsCompleteResponse(endToEndId, actions);

        public void CreateTransctions() => CompanyTransactions = CompanyTransactionsResponse.Create();
    }    
}
