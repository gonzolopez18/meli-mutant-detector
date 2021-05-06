using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Api.Application.Model
{
    public class StatsView
    {
        public StatsView(int count_mutant_dna, int count_human_dna)
        {
            this.count_mutant_dna = count_mutant_dna;
            this.count_human_dna = count_human_dna;
            this.ratio = count_human_dna > 0 ? (decimal)count_mutant_dna / count_human_dna : 0;
        }

        public int count_mutant_dna { get; }
        public int count_human_dna { get; }
        public decimal ratio { get; }
    }
}
