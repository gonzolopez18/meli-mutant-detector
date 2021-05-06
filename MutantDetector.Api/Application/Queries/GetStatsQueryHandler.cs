using MediatR;
using MutantDetector.Api.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Queries
{
    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, StatsView>
    {
        public GetStatsQueryHandler()
        {
        }

        public async Task<StatsView> Handle(GetStatsQuery request, CancellationToken cancellationToken)
        {
            StatsView stats = new StatsView( 130, 200);
            return stats;
        }
    }
}
