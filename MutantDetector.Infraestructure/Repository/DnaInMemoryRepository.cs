using MutantDetector.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantDetector.Infraestructure.Repository
{
    public class DnaInMemoryRepository : IDnaRepository
    {
        private readonly List<Dna> dnaList;
        public DnaInMemoryRepository()
        {
            dnaList = new List<Dna>();
        }
        public List<Dna> Dnas { get { return dnaList; } }

        public async Task<bool> AddAsync(Dna dna)
        {
            Dnas.Add(dna);
            return true;
        }

        public async Task<IQueryable<Dna>> GetAllAsync()
        {
            return Dnas.AsQueryable();
        }

    }
}
