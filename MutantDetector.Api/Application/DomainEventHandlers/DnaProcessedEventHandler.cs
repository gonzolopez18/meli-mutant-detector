using MediatR;
using MutantDetector.Domain.AggregatesModel.Stats;
using MutantDetector.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.DomainEventHandlers
{
    public class DnaProcessedEventHandler : INotificationHandler<DnaProcessedEvent>
    {
        private readonly IStatsRepository _statsRepository;

        public DnaProcessedEventHandler( IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task Handle(DnaProcessedEvent notification, CancellationToken cancellationToken)
        {
            await _statsRepository.AddStatAsync(notification.IsMutant);
        }
    }
}
