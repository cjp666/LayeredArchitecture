using CJSoftware.CrossCutting.Enumerations;

namespace CJSoftware.Application.DataTransfer
{
    public class PersonAddressDTO
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public AddressLocation Location { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Telephone { get; set; }
    }
}