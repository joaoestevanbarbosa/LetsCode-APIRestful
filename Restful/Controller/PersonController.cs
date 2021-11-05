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
        static int PersonIdCount = 0;

        static int AddressIdCount = 0;

        static List<Person> listPeople = new List<Person>();

        static List<Address> listAddress = new List<Address>();


        [HttpGet]
        public IActionResult People([FromQuery] string cpf, [FromQuery] string firstName)
        {
            IEnumerable<Person> result = null;
      
            return Ok(listPeople.Where(x => (string.IsNullOrWhiteSpace(cpf) || x.Cpf == cpf) && (string.IsNullOrWhiteSpace(firstName) || x.FirstName == firstName)));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPerson([FromRoute] int id)
        {
            return Ok(listPeople.Where(x => x.Id == id).FirstOrDefault());
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

            person.Id = PersonIdCount++;

            listPeople.Add(person);
            return StatusCode(201);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdatePerson([FromRoute] int id, [FromBody] Person person)
        {
            try //encapsular erro
            {
                var personUpdate = listPeople.Where(x => x.Id == id).FirstOrDefault();

                listPeople.Remove(personUpdate);

                person.Id = id;

                listPeople.Add(person);

                return Ok();
            }
            catch //encapsular erro
            {
                return StatusCode(500);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {

            var personDelete = listPeople.Where(x => x.Id == id).FirstOrDefault();

            listPeople.Remove(personDelete);

            return Ok();
        }

        [HttpPost]
        [Route("{PersonID}/Address")]
        public IActionResult AddAdress([FromRoute] int PersonId, [FromBody] Address address)
        {
            address.PersonId = PersonId;

            address.Id = AddressIdCount++;

            listAddress.Add(address);

            return Ok();
        }

        [HttpGet]
        [Route("{PersonId}/Address")]
        public IActionResult GetAddress([FromRoute] int PersonId)
        {
            return Ok(listAddress.Where(x => x.PersonId == PersonId));
        }

        [HttpGet]
        [Route("{PersonId}/Address/{id}")]
        public IActionResult GetAddressById([FromRoute] int PersonId, [FromRoute] int id)
        {
            var result = listAddress.Where(x => x.PersonId == PersonId && x.Id == id);

            if (result.Any())
                return Ok(result);
            else
                return StatusCode(204, string.Empty);

        }   

        [HttpDelete]
        [Route("{PersonId}/Address/{id}")]
        public IActionResult DeleteAddressById([FromRoute] int PersonId, [FromRoute] int id)
        {
            var addressToDelete = listAddress.Where(x => x.PersonId == PersonId && x.Id == id).FirstOrDefault();

            listAddress.Remove(addressToDelete);

            return Ok();
        }

        [HttpDelete]
        [Route("{PersonId}/Address")]
        public IActionResult DeleteAddressByPersonId([FromRoute] int PersonId)
        {
            var addressIds = listAddress.Where(x => x.PersonId == PersonId).Select(x => x.Id).ToList();


            foreach (var id in addressIds)
            {
                var addressToDelete = listAddress.Where(x => x.PersonId == PersonId && x.Id == id).FirstOrDefault();

                listAddress.Remove(addressToDelete);
            }

            return Ok();
        }

    }
}
