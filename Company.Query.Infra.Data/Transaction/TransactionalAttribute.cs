using Microsoft.AspNetCore.Mvc;

namespace Company.Query.Infra.Data.Transaction
{
    public class TransactionalAttribute : ServiceFilterAttribute
    {
        public TransactionalAttribute() : base(typeof(TransactionFilter))
        {
        }
    }
}
