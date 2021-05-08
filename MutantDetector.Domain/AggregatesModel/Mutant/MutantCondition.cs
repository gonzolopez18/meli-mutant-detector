using System;
using System.Collections.Generic;
using System.Text;

namespace MutantDetector.Domain.AggregatesModel
{
    public class MutantCondition
    {
        public int MaxSecuenceQty { get{ return 1; } }

        public IEnumerable<string> DnaBases { get { return new List<string>() { "AAAA", "CCCC", "GGGG", "TTTT" }; } }

    }
}
