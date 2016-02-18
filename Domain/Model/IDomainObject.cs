namespace CJSoftware.Domain.Model
{
	/// <summary>
	/// Provides a contract for a domain object.
	/// </summary>
	public interface IDomainObject
	{
	}

	/// <summary>
	/// Provides a contract for a domain object with a strongly typed unique identifier.
	/// </summary>
	/// <typeparam name="TKey">The type of the domain object unique identifier.</typeparam>
	public interface IDomainObject<TKey> : IDomainObject
	{
		/// <summary>
		/// Gets or sets the value of the domain object unique identifier.
		/// </summary>
		TKey Id { get; set; }
	}
}
