using CJSoftware.Domain.Model;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Infrastructure.Repositories
{
	public class CompanyRepository : DbSetRepository<int, Company>,  ICompanyRepository
	{
		public CompanyRepository(IDbSetUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}
	}
}