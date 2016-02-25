namespace CJSoftware.Domain.Model
{
	public abstract class DomainObject :IDomainObject
	{
	}

	/// <summary>
	/// Provides an abstract implementation of a domain object with a strongly typed unique identifier
	/// </summary>
	/// <typeparam name="TKey">The type of the domain object unique identifier</typeparam>
	public abstract class DomainObject<TKey> : DomainObject, IDomainObject<TKey>
	{
		/// <summary>
		/// Gets or sets the value of the domain object unique identifier
		/// </summary>
		public TKey Id { get; set; }
	}
}
