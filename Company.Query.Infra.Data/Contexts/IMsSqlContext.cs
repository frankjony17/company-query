using System;
using System.Data;
using System.Threading.Tasks;

namespace Company.Query.Infra.Data.Contexts
{
    public interface IMsSqlContext
    {
        Task<TResult> ExecuteAsync<TResult>(Func<IDbConnection, Task<TResult>> func);
        TResult Execute<TResult>(Func<IDbConnection, TResult> func);
        Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, dynamic parameters);
    }
}
