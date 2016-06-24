using CJSoftware.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJSoftware.Infrastructure.DatabaseMapping
{
    public class PersonsAddressDbMap : DomainObjectDbMap<int, PersonsAddress>
    {
        public PersonsAddressDbMap()
               : base("PeopleAddresses")
        {
        }

        protected override void MapIgnores()
        {
            base.MapIgnores();

            Ignore(p => p.Location);
        }

        protected override void MapKeys()
        {
            base.MapKeys();

            HasKey(k => k.PersonId);
        }

        protected override void MapProperties()
        {
            base.MapProperties();

            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.PersonId)
                .HasColumnName("PersonId");

#pragma warning disable CS0618 // Type or member is obsolete
            Property(p => p.LocationValue)
                .HasColumnName("Location");
#pragma warning restore CS0618 // Type or member is obsolete

            Property(p => p.Line1)
                .HasColumnName("Line1");

            Property(p => p.Line2)
                .HasColumnName("Line2");

            Property(p => p.Town)
                .HasColumnName("Town");

            Property(p => p.County)
                .HasColumnName("County");

            Property(p => p.Postcode)
                .HasColumnName("Postcode");

            Property(p => p.Telephone)
                .HasColumnName("Telephone");
        }
    }
}