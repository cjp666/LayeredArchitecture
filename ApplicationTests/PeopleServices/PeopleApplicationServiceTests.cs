using CJSoftware.Application.DataTransfer;
using CJSoftware.Application.PeopleServices;
using CJSoftware.Application.Translators;
using CJSoftware.Domain;
using CJSoftware.Domain.Model;
using CJSoftware.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationTests.PeopleServices
{
    [TestClass]
    public class PeopleApplicationServiceTests
    {
        [TestMethod]
        public void GetAll_OnePersonNoAddress_OnePersonDTO()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var peopleRepository = new Mock<IPeopleRepository>();
            var peopleAddressRepository = new Mock<IPeopleAddressesRepository>();
            var personTranslator = new Mock<IPersonTranslator>();
            var personAddressTranslator = new Mock<IPersonAddressTranslator>();

            var person = new People { Id = 4321, EmployeeReference = "ABC123", Name = "Bob", IsActive = true };

            peopleRepository.Setup(r => r.GetAll())
                .Returns(new List<People> { person }.AsEnumerable);

            var service = new PeopleApplicationService(unitOfWork.Object,
                peopleRepository.Object,
                peopleAddressRepository.Object,
                personTranslator.Object,
                personAddressTranslator.Object);

            var results = service.GetAll();

            Assert.AreEqual(1, results.Count());

            peopleRepository.Verify(r => r.GetAll(), Times.Once);
            peopleAddressRepository.Verify(r => r.GetAllForPerson(4321), Times.Once);
            personTranslator.Verify(t => t.Translate(person), Times.Once);
            personAddressTranslator.Verify(t => t.Translate(It.IsAny<PersonsAddress>()), Times.Never);
        }

        [TestMethod]
        public void GetAll_OnePersonTwoAddresses_OnePersonDTO()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var peopleRepository = new Mock<IPeopleRepository>();
            var peopleAddressRepository = new Mock<IPeopleAddressesRepository>();
            var personTranslator = new Mock<IPersonTranslator>();
            var personAddressTranslator = new Mock<IPersonAddressTranslator>();

            var person = new People { Id = 4321, EmployeeReference = "ABC123", Name = "Bob", IsActive = true };

            peopleRepository.Setup(r => r.GetAll())
                .Returns(new List<People> { person }.AsEnumerable);

            var address1 = new PersonsAddress { Id = 9123, PersonId = 4321 };
            var address2 = new PersonsAddress { Id = 9543, PersonId = 4321 };
            peopleAddressRepository.Setup(r => r.GetAllForPerson(4321))
                .Returns(new List<PersonsAddress> { address1, address2 });

            personTranslator.Setup(t => t.Translate(person))
                .Returns(new PeopleDTO());

            var service = new PeopleApplicationService(unitOfWork.Object,
                peopleRepository.Object,
                peopleAddressRepository.Object,
                personTranslator.Object,
                personAddressTranslator.Object);

            var results = service.GetAll();

            Assert.AreEqual(1, results.Count());

            peopleRepository.Verify(r => r.GetAll(), Times.Once);
            peopleAddressRepository.Verify(r => r.GetAllForPerson(4321), Times.Once);
            personTranslator.Verify(t => t.Translate(person), Times.Once);
            personAddressTranslator.Verify(t => t.Translate(It.IsAny<PersonsAddress>()), Times.Exactly(2));
        }

        [TestMethod]
        public void GetAll_TwoPeopleOneAddressEach_TwoPersonDTOs()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var peopleRepository = new Mock<IPeopleRepository>();
            var peopleAddressRepository = new Mock<IPeopleAddressesRepository>();
            var personTranslator = new Mock<IPersonTranslator>();
            var personAddressTranslator = new Mock<IPersonAddressTranslator>();

            var person1 = new People { Id = 4321, EmployeeReference = "ABC123", Name = "Bob", IsActive = true };
            var person2 = new People { Id = 4875, EmployeeReference = "ABC123", Name = "Bob", IsActive = true };

            peopleRepository.Setup(r => r.GetAll())
                .Returns(new List<People> { person1, person2 }.AsEnumerable);

            var person1Address = new PersonsAddress { Id = 9123, PersonId = 4321 };
            var person2Address = new PersonsAddress { Id = 9543, PersonId = 4321 };
            peopleAddressRepository.Setup(r => r.GetAllForPerson(4321))
                .Returns(new List<PersonsAddress> { person1Address });
            peopleAddressRepository.Setup(r => r.GetAllForPerson(4875))
                .Returns(new List<PersonsAddress> { person2Address });

            personTranslator.Setup(t => t.Translate(It.IsAny<People>()))
                .Returns(new PeopleDTO());

            var service = new PeopleApplicationService(unitOfWork.Object,
                peopleRepository.Object,
                peopleAddressRepository.Object,
                personTranslator.Object,
                personAddressTranslator.Object);

            var results = service.GetAll();

            Assert.AreEqual(2, results.Count());

            peopleRepository.Verify(r => r.GetAll(), Times.Once);
            peopleAddressRepository.Verify(r => r.GetAllForPerson(It.IsAny<int>()), Times.Exactly(2));
            peopleAddressRepository.Verify(r => r.GetAllForPerson(4321), Times.Once);
            peopleAddressRepository.Verify(r => r.GetAllForPerson(4875), Times.Once);
            personTranslator.Verify(t => t.Translate(It.IsAny<People>()), Times.Exactly(2));
            personAddressTranslator.Verify(t => t.Translate(It.IsAny<PersonsAddress>()), Times.Exactly(2));
        }
    }
}