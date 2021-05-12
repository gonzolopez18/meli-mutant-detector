using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Dapper
{
    public interface IDapperWrapper
    {
        Task<T> QuerySingle<T>(IDbConnection connection, string sql);

        Task<int> ExecuteAsync(IDbConnection connection, string sql);

        Task<int> ExecuteAsync(IDbConnection connection, string sql, Dictionary<string, object> dictionary);

    }
}
