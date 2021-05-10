using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Domain.AggregatesModel
{
    public interface IDnaRepository
    {
        Task<bool> AddAsync(Dna dna);

        //Task<IQueryable<Dna>> GetAllAsync();

    }
}
