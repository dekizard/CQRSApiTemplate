using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiTemplate.Application.Interfaces;

namespace CQRSApiTemplate.Infrastructure.Persistence
{
    public class ApplicationReadDbFacade : IApplicationReadDbFacade, IDisposable
    {
        private readonly IDbConnection connection;
        private bool disposed = false;

        public ApplicationReadDbFacade(IConfiguration configuration) => connection = new SqlConnection(configuration.GetConnectionString("CQRSApiTemplate"));

        public async Task<IReadOnlyList<TReturn>> QueryAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
                => (await connection.QueryAsync<TReturn>(sql, param, transaction)).AsList();

        public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
                => (await connection.QueryAsync(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType)).AsList();

        public async Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
                => await connection.QueryFirstOrDefaultAsync<TReturn>(sql, param, transaction);

        public async Task<TReturn> QuerySingleAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
                => await connection.QuerySingleAsync<TReturn>(sql, param, transaction);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection.Dispose();
                }

                disposed = true;
            }
        }
    }
}
