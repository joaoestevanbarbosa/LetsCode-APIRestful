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
    public class PersonController : ControllerBase
    {
        static List<Person> listPeople = new List<Person>();


        [HttpGet]
        public IActionResult People([FromQuery] string cpf, [FromQuery] string firstName)
        {
            IEnumerable<Person> result = null;
      
            return Ok(listPeople.Where(x => (string.IsNullOrWhiteSpace(cpf) || x.Cpf == cpf) && (string.IsNullOrWhiteSpace(firstName) || x.FirstName == firstName)));
        }

        [HttpGet]
        [Route("{index}")]
        public IActionResult GetPerson([FromRoute] int index)
        {
            return Ok(listPeople[index]); //pegando pessoa pelo index, deserealizando o json
        }

        [HttpPost]
        public IActionResult Add ([FromBody] Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
                return StatusCode(400, $"O campo {nameof(person.FirstName)} é obrigatório");

            if (string.IsNullOrWhiteSpace(person.LastName))
                return StatusCode(400, $"O campo {nameof(person.LastName)} é obrigatório");

            if (person.Cpf?.Length != 11)
                return StatusCode(400, $"O campo {nameof(person.Cpf)} não está no padrão");

            listPeople.Add(person);
            return StatusCode(201);
        }

        [HttpPatch]
        [Route("{index}")]
        public IActionResult UpdatePerson([FromRoute] int index, [FromBody] Person person)
        {
            try //encapsular erro
            {
                listPeople[index] = person;
                return Ok();
            }
            catch //encapsular erro
            {
                return StatusCode(500);
            }


            listPeople[index] = person;
            return Ok();
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeletePerson([FromRoute] int index)
        {
            listPeople.RemoveAt(index);
            return Ok();
        }
    }
}
