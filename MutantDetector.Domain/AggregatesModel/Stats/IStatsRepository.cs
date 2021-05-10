using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MutantDetector.Domain.AggregatesModel.Stats
{
    public interface IStatsRepository
    {
        public Task<Stats> GetStatsAsync();

        public Task AddStatAsync(bool isMutant);
    }
}
