using Dapper;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Infraestructure.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Repository
{
    public class DapperWrapper : IDapperWrapper
    {

         public async Task<int> ExecuteAsync(IDbConnection connection, string sql)
        {
            return await connection.ExecuteAsync(sql);
        }

        public async Task<int> ExecuteAsync(IDbConnection connection, string sql, Dictionary<string, object> dictionary)
        {
            var parameters = new DynamicParameters(dictionary);
            return await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<T> QuerySingle<T>(IDbConnection connection, string sql)
        {
            return (T) (await connection.QueryAsync<T>(sql)).SingleOrDefault();
        }

    }
}
