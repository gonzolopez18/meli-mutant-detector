using MutantDetector.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Repository
{
    public class DnaRepository : IDnaRepository
    {
        public Task<Dna> AddAsync(Dna dna)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Dna>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
