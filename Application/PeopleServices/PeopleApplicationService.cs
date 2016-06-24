using System.Collections.Generic;
using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain;
using CJSoftware.Domain.Repositories;
using CJSoftware.Application.Translators;

namespace CJSoftware.Application.PeopleServices
{
    public class PeopleApplicationService : ApplicationService, IPeopleApplicationService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPeopleAddressesRepository _peopleAddressesRepository;
        private readonly IPersonTranslator _personTranslator;
        private readonly IPersonAddressTranslator _personAddressTranslator;

        public PeopleApplicationService(IUnitOfWork unitOfWork, IPeopleRepository peopleRepository,
            IPeopleAddressesRepository peopleAddressesRepository,
            IPersonTranslator personTranslator,
            IPersonAddressTranslator personAddressTranslator)
            : base(unitOfWork)
        {
            _peopleRepository = peopleRepository;
            _peopleAddressesRepository = peopleAddressesRepository;
            _personTranslator = personTranslator;
            _personAddressTranslator = personAddressTranslator;
        }

        public IEnumerable<PersonDTO> GetAll()
        {
            var people = _peopleRepository.GetAll();

            var results = new List<PersonDTO>();
            foreach(var person in people)
            {
                var personDTO = _personTranslator.Translate(person);

                var addresses = _peopleAddressesRepository.GetAllForPerson(person.Id);

                foreach(var address in addresses)
                {
                    var addressDTO = _personAddressTranslator.Translate(address);
                    personDTO.Addresses.Add(addressDTO);
                }

                results.Add(personDTO);
            }

            return results;
        }
    }
}