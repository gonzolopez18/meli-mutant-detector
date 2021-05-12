using System;
using System.Collections.Generic;
using System.Text;

namespace MutantDetector.Domain.AggregatesModel.Stats
{
    public class Stats
    {
        public Guid Id { get; set; }
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public DateTimeOffset LastUpdate { get; set; }

    }
}
