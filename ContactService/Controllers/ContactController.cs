using ContactService.Data;
using ContactService.Models;
using ContactService.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        IRepo _repo;

        public ContactController(IRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(PersonDTO person)
        {
            var result =  await _repo.CreatePerson(new Person { Company = person.Company,Name = person.Name, Surname = person.Surname});
            if (result)
                return Ok();
            return Problem();
            
        }

        [HttpDelete]
        public async Task<ActionResult> RemovePerson(string Name, string Surname)
        {
            var result = await _repo.DeletePerson(new Person{ Name=Name, Surname=Surname});

            if (result)
                return Ok();
            return Problem();

        }

        [HttpPost]
        public async Task<ActionResult> CreatePersonContact(PersonWithContact person)
        {
            var result = await _repo.CreateContact(person);

            if (result)
                return Ok();
            return Problem();
        }

        [HttpDelete]
        public async Task<ActionResult> RemovePersonContact(PersonWithContact contact)
        {
            var result = await _repo.RemoveContact(contact);

            if (result)
                return Ok();
            return Problem();
        }

        [HttpGet]
        public async Task<ActionResult> GetPerson()
        {
            var result = await _repo.GetPersons();
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult> GetPersonDetail()
        {
            var result = await _repo.GetPersonWithContact();
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }
    }
}
