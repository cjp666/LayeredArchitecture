using CJSoftware.Domain.Model;
using CJSoftware.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CJSoftware.Infrastructure.Repositories
{
    public class PeopleAddressesRepository : DbSetRepository<int, PersonsAddress>, IPeopleAddressesRepository
    {
        public PeopleAddressesRepository(IDbSetUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<PersonsAddress> GetAllForPerson(int personId)
        {
            var result = Query()
                .Where(p => p.PersonId == personId);

            return result;
        }
    }
}
