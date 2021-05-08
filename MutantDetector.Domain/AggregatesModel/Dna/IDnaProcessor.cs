using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Domain.AggregatesModel
{
    public interface IDnaProcessor
    {
        public bool isMutant(IEnumerable<string> Dna);

    }
}
