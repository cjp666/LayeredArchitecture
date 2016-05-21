using CJSoftware.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJSoftware.Infrastructure.DatabaseMapping
{
    public class PeopleDbMap : DomainObjectDbMap<int, People>
    {
        public PeopleDbMap()
            : base("People")
        {
        }

        protected override void MapProperties()
        {
            base.MapProperties();

            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.EmployeeReference)
                .HasColumnName("EmployeeReference")
                .IsRequired();

            Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();

            Property(p => p.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();
        }
    }
}