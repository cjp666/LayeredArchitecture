using System.Collections.Generic;

namespace CJSoftware.Application.DataTransfer
{
    public class PersonDTO
    {
        public int Id { get; set; }

        public string EmployeeReference { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public List<PersonAddressDTO> Addresses { get; } = new List<PersonAddressDTO>();

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Name);
        }
    }
}