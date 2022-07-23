using Microsoft.AspNetCore.Mvc.Filters;
using Company.Query.Infra.CrossCutting.Interfaces.Exception;
using Company.Query.Infra.Data.Contexts;
using System.Data;

namespace Company.Query.Infra.Data.Transaction
{
    public class TransactionFilter : IActionFilter
    {

        private IDbTransaction _transaction;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _transaction = _unitOfWork.Begin();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && !(context.Exception is IIgnoreRollbackException))
            {
                _transaction.Rollback();
                _unitOfWork.Dispose();
                return;
            }

            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                _unitOfWork.Dispose();
                throw;
            }
        }
    }
}
