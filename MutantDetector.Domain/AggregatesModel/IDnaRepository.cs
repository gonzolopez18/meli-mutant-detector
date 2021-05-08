using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Domain.AggregatesModel
{
    public interface IDnaRepository
    {
        Task<Dna> AddAsync(Dna dna);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
