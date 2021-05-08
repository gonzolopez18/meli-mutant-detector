using System;
using System.Collections.Generic;
using System.Text;

namespace MutantDetector.Domain.AggregatesModel
{
    public class Dna
    {
        public string DnaSecuence { get; set; }

        public bool IsMutant { get; set; }
    }
}
