using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MutantDetector.Domain.DomainEvents
{
    public class DnaProcessedEvent : INotification
    {
        public bool IsMutant { get; set; }

        public DnaProcessedEvent(bool isMutant)
        {
            IsMutant = isMutant;
        }
    }
}
