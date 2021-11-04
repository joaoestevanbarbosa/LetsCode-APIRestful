using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello([FromQuery] string firstName, [FromQuery] string lastName)
        {
            
            if (string.IsNullOrWhiteSpace(firstName))
                return StatusCode(400, $"Informe o argumento '{nameof(firstName)}'");

            return Ok($"Olá,{firstName} {lastName}!"); //é pra retornar 200
        }

        [HttpGet]
        [Route("Person")]
        public IActionResult Person([FromQuery]Person person)
        {
            return Ok($"{person.FirstName} {person.LastName}");
        }

        [HttpPost()]
        [Route("Person/{firstName}/{lastName}")]
        public IActionResult Person2 ([FromRoute] string firstName, [FromRoute] string lastName)
        {
            return Ok($"POST: {firstName} {lastName}");
        }
    }
}
