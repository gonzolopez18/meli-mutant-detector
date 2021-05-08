using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MutantDetector.Api.Application.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MutantDetector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mutantController : ControllerBase
    {

        private readonly IMediator _mediator;

        public mutantController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Detecta si la matriz de ADN recibida pertenece a un mutante o no. Si se encuentra 
        /// más de una secuencia de cuatro letras iguales, es un mutante.
        /// </summary>
        /// <param name="value">Array de strings que representa una matriz de NxN elementos.
        /// Los únicos caracteres permitidos son A, T, C, y G.</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CheckMutantCommand dna)
        {
            try
            {
                bool result = await _mediator.Send(dna);

                if (!result)
                    return StatusCode(403);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(403);
            }
        }
    }
}
