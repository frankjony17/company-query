
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Company.Query.Infra.Data.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool isDisposed = false;

        public UnitOfWork(string connectionString)
        {
            _connection ??= new SqlConnection(connectionString);
            _connection.Open();
        }

        public async Task ExecuteAsync(string sql, object parameters)
            => await _connection.ExecuteAsync(sql, parameters, transaction: _transaction);

        public async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters)
            => await _connection.QueryFirstOrDefaultAsync<TResult>(sql, parameters, transaction: _transaction);

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters)
         => await _connection.QueryAsync<TResult>(sql, parameters, transaction: _transaction);

        public IDbTransaction Begin() => _transaction = _connection.BeginTransaction();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                isDisposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
