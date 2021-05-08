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
    public class StatsRepository : IStatsRepository
    { 
        private readonly IDnaRepository _repository;

        public StatsRepository(IDnaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Stats> GetStats()
        {
            int mutants =  _repository.GetAllAsync().GetAwaiter().GetResult()
                    .Where(x => x.IsMutant ).Count();
            int humans = _repository.GetAllAsync().GetAwaiter().GetResult()
                    .Where(x => x.IsMutant == false).Count();

            return new Stats(mutants, humans);
        }

    }
}
