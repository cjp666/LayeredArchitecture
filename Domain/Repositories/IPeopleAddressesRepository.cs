using CJSoftware.Domain.Model;
using System.Collections.Generic;

namespace CJSoftware.Domain.Repositories
{
    public interface IPeopleAddressesRepository : IRepository<int, PersonsAddress>
    {
        IEnumerable<PersonsAddress> GetAllForPerson(int personId);
    }
}