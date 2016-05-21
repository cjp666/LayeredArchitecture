using CJSoftware.Application.DataTransfer;
using CJSoftware.Application.PeopleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CJSoftware.WebServices.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IPeopleApplicationService _peopleApplicationService;

        public PeopleController(IPeopleApplicationService peopleApplicationService)
        {
            _peopleApplicationService = peopleApplicationService;
        }

        [HttpGet]
        public IEnumerable<PeopleDTO> GetAll()
        {
            var people = _peopleApplicationService.GetAll();

            return people;
        }
    }
}