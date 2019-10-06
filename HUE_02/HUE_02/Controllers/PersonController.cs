using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HUE_02.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class PersonController : ControllerBase
    {
        private static readonly List<Person> Persons = new List<Person>
        {
            // Avoid checking in real names and email addresses to GitHub, might be misused
            new Person
            {
                ID=0,
                FirstName="Lukas",
                LastName="Rauchenzauner",
                Email="rauchi13.lr@gmail.com"
            },
            new Person
            {
                ID=1,
                FirstName="Matthias",
                LastName="Heiden",
                Email="matthias.heiden@protonmail.com"
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Persons);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Person newContact)
        {
            // Prefer `.Any`
            if (Persons.Where(person => person.ID == newContact.ID).Count() == 0)
            {
                Persons.Add(newContact);
                return Created("", newContact);
            }

            return BadRequest("Invalid input");
        }

        [HttpGet]
        [Route("findByName/{name}", Name = "GetSpecificContact")]
        public IActionResult GetContactByName(String name)
        {
            for(int i=0; i< Persons.Count(); i++)
            {
                // Spec: Contains, not equal
                if (Persons.ElementAt(i).FirstName.CompareTo(name) == 0 || Persons.ElementAt(i).LastName.CompareTo(name) == 0)
                {
                    return Ok(Persons.ElementAt(i));
                }
            }
            return BadRequest("Invalid name");
        }

        [HttpDelete]
        [Route("{index}", Name = "DeleteSpecificContact")]
        public IActionResult DeleteContact(int index)
        {
            if(index >= 0 && index < Persons.Count())
            {
                Persons.RemoveAt(index);
                return NoContent();
            }
            return BadRequest("Invalid Index");
        }
    }
}
