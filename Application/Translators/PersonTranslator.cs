using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;
using System;

namespace CJSoftware.Application.Translators
{
    public class PersonTranslator : IPersonTranslator, ITranslator<Person, PersonDTO>
    {
        public PersonDTO Translate(Person person)
        {
            var dto = new PersonDTO
            {
                Id = person.Id,
                EmployeeReference = person.EmployeeReference,
                Name = person.Name,
                IsActive = person.IsActive
            };

            return dto;
        }
    }
}
