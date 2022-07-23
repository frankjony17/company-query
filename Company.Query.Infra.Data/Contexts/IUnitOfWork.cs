using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Company.Query.Infra.Data.Contexts
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction Begin();
        Task ExecuteAsync(string sql, object parameters);
        Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters);
    }
}
