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
    public class DnaRepository : IDnaRepository
    {

        private readonly ISqlConnectionFactory _connectionFactory;

        public DnaRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> AddAsync(Dna dna)
        {
            try
            {

                using (IDbConnection db = _connectionFactory.GetOpenConnection())
                {
                    string sqlQuery = @"INSERT INTO [dbo].[Dna]
                                            (ID, [DnaSecuence],[IsMutant])
                                        VALUES(NEWID(), @DnaSecuence , @IsMutant)";

                    int rowsAffected = await db.ExecuteAsync(sqlQuery, new { dna.DnaSecuence, dna.IsMutant });
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        //public Task<IQueryable<Dna>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
