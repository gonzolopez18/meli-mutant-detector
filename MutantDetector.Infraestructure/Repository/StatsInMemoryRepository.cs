using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Domain.AggregatesModel.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Repository
{
    public class StatsInMemoryRepository : IStatsRepository
    {
        private int mutantsQty;
        private int humansQty;


        public async Task AddStatAsync(bool isMutant)
        {
            if (isMutant)
            {
                ++mutantsQty;
            }else
            {
                ++humansQty;
            }

        }

        public async Task<Stats> GetStatsAsync()
        {
            return new Stats() { count_mutant_dna = mutantsQty, count_human_dna = humansQty };
        }
    
    }
}
