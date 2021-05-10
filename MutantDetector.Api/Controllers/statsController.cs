using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MutantDetector.Api.Application.Model;
using MutantDetector.Api.Application.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutantDetector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class statsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public statsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Devuelve un Json con las estadísticas de las
        ///verificaciones de ADN: {“count_mutant_dna”:40, “count_human_dna”:100: “ratio”:0.4}
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<StatsView>> GetStats()
        {
            try
            {
                StatsView statitics = await _mediator.Send(new GetStatsQuery());

                return Ok(statitics);

            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

    
    }
}
