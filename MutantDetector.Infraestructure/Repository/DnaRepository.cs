using Dapper;
using MutantDetector.Domain.AggregatesModel;
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
    public class DnaRepository : IDnaRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        private readonly string ConnectionString;
        
        public DnaRepository(string connectionString, IDapperWrapper dapperWrapper)
        {
            ConnectionString = connectionString;
            _dapperWrapper = dapperWrapper;
        }

        public async Task<bool> AddAsync(Dna dna)
        {
            try
            {

                var dictionary = new Dictionary<string, object>
                        {
                            { "@DnaSecuence", dna.DnaSecuence },
                            { "@IsMutant", dna.IsMutant },
                        };

                using (var connection = new SqlConnection(ConnectionString))
                {
                    string sqlQuery = @"INSERT INTO [dbo].[Dna]
                                            (ID, [DnaSecuence],[IsMutant])
                                        VALUES(NEWID(), @DnaSecuence , @IsMutant)";

                    int rowsAffected = await _dapperWrapper.ExecuteAsync(
                        connection, sqlQuery, dictionary);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return true;
        }


    }
}
