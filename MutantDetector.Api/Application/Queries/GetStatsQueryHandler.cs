using MediatR;
using MutantDetector.Api.Application.Model;
using MutantDetector.Domain.AggregatesModel.Stats;
using MutantDetector.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Queries
{
    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, StatsView>
    {
        private readonly IStatsRepository _statsRepository;
        public GetStatsQueryHandler(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<StatsView> Handle(GetStatsQuery request, CancellationToken cancellationToken)
        {

            Stats stats = await _statsRepository.GetStatsAsync();

            return new StatsView(stats.count_mutant_dna, stats.count_human_dna, stats.ratio);
        }
    }
}
