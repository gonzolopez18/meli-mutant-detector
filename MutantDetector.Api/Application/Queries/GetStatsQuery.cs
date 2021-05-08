using MediatR;
using MutantDetector.Api.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Queries
{
    public class GetStatsQuery : IRequest<StatsView>
    {
        public GetStatsQuery()
        {
        }
    }
}
