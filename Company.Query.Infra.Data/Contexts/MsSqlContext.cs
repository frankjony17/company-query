using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Company.Query.Infra.Data.Contexts
{
    [ExcludeFromCodeCoverage]
    public class MsSqlContext : IMsSqlContext, IDisposable
    {
        private readonly string _connectionStrings;
        private IDbConnection _connection;

        public MsSqlContext(string connectionString)
        {
            _connectionStrings = connectionString;
        }

        public async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters) => await GetConnection().QueryFirstOrDefaultAsync<TResult>(sql, parameters);

        public async Task<TResult> ExecuteAsync<TResult>(Func<IDbConnection, Task<TResult>> func) => await func(GetConnection());

        public TResult Execute<TResult>(Func<IDbConnection, TResult> func)
        {
            return func(GetConnection());
        }

        protected virtual IDbConnection GetConnection() => OpenConnection();

        private void CreateConnection()
        {
            _connection ??= new SqlConnection(_connectionStrings);
        }

        private IDbConnection OpenConnection()
        {
            CreateConnection();

            if (!_connection.State.Equals(ConnectionState.Open))
            {
                _connection.Open();
            }

            return _connection;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
