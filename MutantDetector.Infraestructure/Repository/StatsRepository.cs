﻿using Dapper;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Domain.AggregatesModel.Stats;
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
    public class StatsRepository : IStatsRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public StatsRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddStatAsync(bool isMutant)
        {
            int humans = 0;
            int mutants = 0;
            try
            {
                using (IDbConnection db = _connectionFactory.GetOpenConnection())
                {
                    string sqlQueryGet = @"select count_human_dna , count_mutant_dna  
                                            from Stats";

                    Stats data = (await db.QueryAsync<Stats>(sqlQueryGet)).SingleOrDefault();

                    if (data == null || data.Id== null)
                    {
                        string insertQuery = @" 
                                INSERT INTO Stats (Id, count_human_dna, count_mutant_dna)
                                VALUES ('00000000-0000-0000-0000-000000000000', 0, 0)";

                        await db.ExecuteAsync(insertQuery);

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
                        

                    string updateQuery = @"
                        UPDATE Stats SET count_human_dna = @humans, count_mutant_dna = @mutants
                          WHERE Id = '00000000-0000-0000-0000-000000000000'";
                    
                    await db.ExecuteAsync(updateQuery, new { humans, mutants});

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<Stats> GetStatsAsync()
        {
            int humansQty = 0;
            int mutantsQty = 0;
            try
            {

                using (IDbConnection db = _connectionFactory.GetOpenConnection())
                {
                    string sqlQuery = @"select count_human_dna , count_mutant_dna 
                            from Stats where Id = '00000000-0000-0000-0000-000000000000'";

                    Stats data = (await db.QueryAsync<Stats>(sqlQuery)).SingleOrDefault();

                    return data ?? new Stats();
                }
            }
            catch (Exception e)
            {
                //return new Stats(0, 0); ;
                throw e;
            }
        }
    
    }
}
