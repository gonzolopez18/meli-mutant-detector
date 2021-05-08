using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MutantDetector.Domain.AggregatesModel;

namespace MutantDetector.Api.Application.Commands
{
    public class CheckMutantCommandHandler : IRequestHandler<CheckMutantCommand, bool>
    {
        private readonly IDnaRepository _dnaRepository;
        private readonly IDnaProcessor _dnaProcessor;

        public CheckMutantCommandHandler(IDnaRepository dnaRepository, IDnaProcessor dnaProcessor)
        {
            _dnaProcessor = dnaProcessor;
            _dnaRepository = dnaRepository;
        }
        public async Task<bool> Handle(CheckMutantCommand request, CancellationToken cancellationToken)
        {
            bool isMutant = _dnaProcessor.isMutant(request.dna);

            await _dnaRepository.AddAsync(new Dna() { DnaSecuence = request.dna.ToString(), IsMutant = isMutant });

            return isMutant;
        }

    }
}
