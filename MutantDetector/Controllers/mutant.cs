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
    public class mutant : ControllerBase
    {
        
        /// <summary>
        /// Detecta si la matriz de ADN recibida pertenece a un mutante o no. Si se encuentra 
        /// más de una secuencia de cuatro letras iguales, es un mutante.
        /// </summary>
        /// <param name="value">Array de strings que representa una matriz de NxN elementos.
        /// Los únicos caracteres permitidos son A, T, C, y G.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
