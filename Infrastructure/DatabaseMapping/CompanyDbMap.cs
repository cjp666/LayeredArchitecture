using System.ComponentModel.DataAnnotations.Schema;
using CJSoftware.Domain.Model;

namespace CJSoftware.Infrastructure.DatabaseMapping
{
	public class CompanyDbMap : DomainObjectDbMap<int, Company>
	{
		public CompanyDbMap()
			: base("Company")
		{
		}

		protected override void MapProperties()
		{
			base.MapProperties();

			Property(p => p.Id)
				.HasColumnName("Id")
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(p => p.Code)
				.HasColumnName("Code")
				.IsRequired();

			Property(p => p.Name)
				.HasColumnName("Name")
				.IsRequired();

			Property(p => p.EmailAddress)
				.HasColumnName("EmailAddress")
				.IsRequired();
		}
	}
}