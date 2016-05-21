using CJSoftware.Domain.Model;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Infrastructure.Repositories
{
    public class PeopleRepository : DbSetRepository<int, People>, IPeopleRepository
    {
        public PeopleRepository(IDbSetUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}