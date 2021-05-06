using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutantDetector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class stats : ControllerBase
    {
        /// <summary>
        /// Devuelve un Json con las estadísticas de las
        ///verificaciones de ADN: {“count_mutant_dna”:40, “count_human_dna”:100: “ratio”:0.4}
    /// </summary>
    /// <returns></returns>
    [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>() { "value1", "value2" };
        }

    
    }
}
