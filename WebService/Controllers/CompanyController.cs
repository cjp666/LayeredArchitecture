using CJSoftware.Application.CompanyServices;
using System.Web.Http;

namespace WebService.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompanyController(ICompanyApplicationService companyApplicationService)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var results = _companyApplicationService.GetAll();

            return Ok(results);
        }
    }
}
