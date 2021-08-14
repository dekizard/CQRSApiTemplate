using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Interfaces
{
    public interface IApplicationReadDbFacade
    {
        Task<IReadOnlyList<TReturn>> QueryAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<TReturn> QuerySingleAsync<TReturn>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
