using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        [HttpPost]
        public ActionResult CreatePerson()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult RemovePerson()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult CreatePersonContact()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult RemovePersonContact()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult GetPerson()
        {
            return Ok();
        }
        [HttpGet]
        public ActionResult GetPersonDetail()
        {
            return Ok();
        }
    }
}
