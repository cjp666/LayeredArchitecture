using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;
using System;

namespace CJSoftware.Application.Translators
{
    public class PersonTranslator : IPersonTranslator, ITranslator<People, PeopleDTO>
    {
        public PeopleDTO Translate(People person)
        {
            var dto = new PeopleDTO
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
