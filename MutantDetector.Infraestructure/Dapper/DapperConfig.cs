using System;
using System.Collections.Generic;
using System.Text;

namespace MutantDetector.Infraestructure.Dapper
{
    public class DapperConfig
    {
        public string ConnectionString { get; set; }
        public int Timeout { get; set; } = 60;
    }
}
