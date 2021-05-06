using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Commands
{
    public class CheckMutantCommandHandler : IRequestHandler<CheckMutantCommand, bool>
    {
        public async Task<bool> Handle(CheckMutantCommand request, CancellationToken cancellationToken)
        {
            return false;
        }
    }
}
