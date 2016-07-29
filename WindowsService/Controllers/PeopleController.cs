using CJSoftware.Application.DataTransfer;
using CJSoftware.Application.PeopleServices;
using System.Collections.Generic;
using System.Web.Http;

namespace WindowsService.Controllers
{ 
    public class PeopleController : ApiController
    {
        private readonly IPeopleApplicationService _peopleApplicationService;

        public PeopleController(IPeopleApplicationService peopleApplicationService)
        {
            _peopleApplicationService = peopleApplicationService;
        }

        [HttpGet]
        public IEnumerable<PersonDTO> GetAll()
        {
            var people = _peopleApplicationService.GetAll();

            return people;
        }
    }
}
