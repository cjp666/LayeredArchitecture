using System;
using System.Collections.Generic;
using System.Web.Http;
using CJSoftware.Domain.Model;

namespace CJSoftware.WebServices.Controllers
{
	public class CompanyController : ApiController
	{
		[HttpGet]
		public IEnumerable<Company> Get()
		{
			throw new NotImplementedException();			
		}
	}
}