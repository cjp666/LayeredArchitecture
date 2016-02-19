using CJSoftware.Domain.Model;

namespace CJSoftware.Infrastructure.DatabaseMapping
{
	/// <summary>
	///     Provides a base for entity configuration of <see cref="DomainObject" /> instances.
	/// </summary>
	public abstract class DomainObjectDbMap<TDomainObject> : EntityTypeConfigurationBase<TDomainObject>
		where TDomainObject : DomainObject
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="DomainObjectDbMap{TDomainObject}" /> class
		///     with the default table name.
		/// </summary>
		protected DomainObjectDbMap()
			: this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainObjectDbMap{TDomainObject}" /> class.
		/// </summary>
		/// <param name="tableName">The name of the table to map to.</param>
		protected DomainObjectDbMap(string tableName)
			: base(tableName)
		{
		}
	}

	/// <summary>
	///     Provides a base for entity configuration of <see cref="DomainObject{TKey}" /> instances.
	/// </summary>
	/// <typeparam name="TKey">The type of Id key present on the specified domain object.</typeparam>
	/// <typeparam name="TDomainObject">The type of domain object being mapped.</typeparam>
	public abstract class DomainObjectDbMap<TKey, TDomainObject> : DomainObjectDbMap<TDomainObject>
		where TDomainObject : DomainObject<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="DomainObjectDbMap{TKey, TDomainObject}" /> class
		///     with the default table name.
		/// </summary>
		protected DomainObjectDbMap()
			: base(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="DomainObjectDbMap{TKey, TDomainObject}" /> class.
		/// </summary>
		/// <param name="tableName">The name of the table to map to.</param>
		protected DomainObjectDbMap(string tableName)
			: base(tableName)
		{
		}

		/// <summary>
		///     Maps the keys of the entity to the relevant columns in the associated table.
		/// </summary>
		protected override void MapKeys()
		{
			base.MapKeys();

			HasKey(x => x.Id);
		}
	}
}