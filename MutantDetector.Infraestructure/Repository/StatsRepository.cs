using Dapper;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Domain.AggregatesModel.Stats;
using MutantDetector.Infraestructure.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly string ConnectionString;

        public StatsRepository(string connectionString, IDapperWrapper dapperWrapper)
        {
            ConnectionString = connectionString;
            _dapperWrapper = dapperWrapper;
        }

        public async Task AddStatAsync(bool isMutant)
        {
            int humans = 0;
            int mutants = 0;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    string sqlQueryGet = "select count_human_dna , count_mutant_dna from Stats";

                    Stats data = await _dapperWrapper.QuerySingle<Stats>(connection, sqlQueryGet);

                    if (data == null || data.Id== null)
                    {
                        string insertQuery = "INSERT INTO Stats (Id, count_human_dna, count_mutant_dna)" +
                            " VALUES ('00000000-0000-0000-0000-000000000000', 0, 0)";

                        await _dapperWrapper.ExecuteAsync(connection, insertQuery);

                    }
                    else
                    {
                        humans = data.count_human_dna;
                        mutants = data.count_mutant_dna;
                    }

                    if (isMutant == true)
                    {
                        mutants++;
                    }
                    else
                    {
                        humans++;
                    }

                    var dictionary = new Dictionary<string, object>
                        {
                            { "@humans", humans },
                            { "@mutants", mutants },
                        };
                    string updateQuery = "UPDATE Stats SET count_human_dna = @humans, " +
                        "count_mutant_dna = @mutants WHERE Id = '00000000-0000-0000-0000-000000000000'";
                    
                    await _dapperWrapper.ExecuteAsync(connection, updateQuery, dictionary );

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<Stats> GetStatsAsync()
        {
            try
            {

                using (var connection = new SqlConnection(ConnectionString))
                {
                    string sqlQuery = "select count_human_dna , count_mutant_dna " +
                        "from Stats where Id = '00000000-0000-0000-0000-000000000000'";

                    Stats data = await _dapperWrapper.QuerySingle<Stats>(connection, sqlQuery);

                    return data ?? new Stats();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
    }
}
