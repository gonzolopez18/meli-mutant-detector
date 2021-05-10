using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MutantDetector.Domain.AggregatesModel;
using MutantDetector.Domain.DomainEvents;

namespace MutantDetector.Api.Application.Commands
{
    public class CheckMutantCommandHandler : IRequestHandler<CheckMutantCommand, bool>
    {
        private readonly IDnaRepository _dnaRepository;
        private readonly IDnaProcessor _dnaProcessor;
        private readonly IMediator _mediator;

        public CheckMutantCommandHandler(IDnaRepository dnaRepository, IDnaProcessor dnaProcessor, IMediator mediator)
        {
            _dnaProcessor = dnaProcessor;
            _dnaRepository = dnaRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(CheckMutantCommand request, CancellationToken cancellationToken)
        {
            bool isMutant = _dnaProcessor.isMutant(request.dna);

            bool result = await _dnaRepository.AddAsync(new Dna() { DnaSecuence = string.Join("|", request.dna.ToArray()), IsMutant = isMutant });

            if (result)
                await _mediator.Publish(new DnaProcessedEvent(isMutant));

             await _mediator.Publish(new DnaProcessedEvent(isMutant));

            return isMutant;
        }

    }
}
