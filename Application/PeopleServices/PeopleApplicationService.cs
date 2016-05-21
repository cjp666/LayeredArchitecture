using System.Collections.Generic;
using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Application.PeopleServices
{
    public class PeopleApplicationService : ApplicationService, IPeopleApplicationService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleApplicationService(IUnitOfWork unitOfWork, IPeopleRepository peopleRepository)
            : base(unitOfWork)
        {
            _peopleRepository = peopleRepository;
        }

        public IEnumerable<PeopleDTO> GetAll()
        {
            var people = _peopleRepository.GetAll();

            var results = new List<PeopleDTO>();
            foreach(var person in people)
            {
                results.Add(new PeopleDTO
                {
                    Id = person.Id,
                    EmployeeReference = person.EmployeeReference,
                    Name = person.Name,
                    IsActive = person.IsActive
                });
            }

            return results;
        }
    }
}