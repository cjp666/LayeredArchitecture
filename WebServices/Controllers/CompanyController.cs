using System.Collections.Generic;
using System.Web.Http;
using CJSoftware.Application.CompanyServices;
using CJSoftware.Application.DataTransfer;

namespace CJSoftware.WebServices.Controllers
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