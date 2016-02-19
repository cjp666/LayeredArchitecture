using System.Collections.Generic;
using CJSoftware.Application.DataTransfer;

namespace CJSoftware.Application.CompanyServices
{
	public interface ICompanyApplicationService
	{
		IEnumerable<CompanyDTO> GetAll();
	}
}
