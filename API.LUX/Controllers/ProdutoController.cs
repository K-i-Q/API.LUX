using API.LUX.Controllers.RequestExamples;
using Domain.LUX.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.LUX.Controllers
{
    [ApiController]
    [Route("v1/produto")]
    public class ProdutoController : Controller
    {



        [ProducesResponseType(typeof(ProdutoDTOResponse), 200)]
        [SwaggerRequestExample(typeof(ProdutoDTO), typeof(ProdutoRequestExample))]
        [HttpPost("inserir")]
        public IActionResult Inserir([FromBody] ProdutoDTO request)
        {
            return Ok();
        }
    }
}
