using CJSoftware.Application.DataTransfer;
using CJSoftware.Domain.Model;

namespace CJSoftware.Application.Translators
{
    public class PersonAddressTranslator : IPersonAddressTranslator
    {
        public PersonAddressDTO Translate(PersonsAddress address)
        {
            var dto = new PersonAddressDTO
            {
                Id = address.Id,
                PersonId = address.PersonId,
                Location = address.Location,
                Line1 = address.Line1,
                Line2 = address.Line2,
                Town = address.Town,
                County = address.County,
                Postcode = address.Postcode,
                Telephone = address.Telephone
            };

            return dto;
        }
    }
}