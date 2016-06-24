using CJSoftware.CrossCutting.Enumerations;
using System;

namespace CJSoftware.Domain.Model
{
    public class PersonsAddress : DomainObject<int>
    {
        public int PersonId { get; set; }

        [Obsolete("Use Location")]
        public int LocationValue { get; set; }

        public AddressLocation Location
        {
#pragma warning disable CS0618 // Type or member is obsolete
            get { return (AddressLocation)LocationValue; }
            set { LocationValue = (int)value; }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Telephone { get; set; }
    }
}