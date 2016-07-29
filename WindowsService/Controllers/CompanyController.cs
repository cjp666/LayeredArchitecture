using CJSoftware.Application.CompanyServices;
using CJSoftware.Application.DataTransfer;
using System.Collections.Generic;
using System.Web.Http;

namespace WindowsService.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompanyController(ICompanyApplicationService companyApplicationService)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        public IEnumerable<CompanyDTO> Get()
        {
            var results = _companyApplicationService.GetAll();

            return results;
        }
    }
}
