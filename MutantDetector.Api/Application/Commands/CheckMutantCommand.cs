using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Commands
{
    public class CheckMutantCommand : IRequest<bool>
    {
        public IEnumerable<string> dna { get; set; }

    }
}
