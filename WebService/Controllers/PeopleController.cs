using CJSoftware.Application.PeopleServices;
using System.Web.Http;

namespace WebService.Controllers
{
    [Authorize]
    public class PeopleController : ApiController
    {
        private readonly IPeopleApplicationService _peopleApplicationService;

        public PeopleController(IPeopleApplicationService peopleApplicationService)
        {
            _peopleApplicationService = peopleApplicationService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var people = _peopleApplicationService.GetAll();

            return Ok(people);
        }
    }
}
