using CJSoftware.Application.DataTransfer;
using System.Collections.Generic;

namespace CJSoftware.Application.PeopleServices
{
    public interface IPeopleApplicationService
    {
        IEnumerable<PeopleDTO> GetAll();
    }
}
