using System.Data.Entity;
using CJSoftware.Domain;
using CJSoftware.Domain.Model;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	/// Provides a contract for a logical unit of work to control database access within an application.
	/// </summary>
	public interface IDbSetUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Retrieves a database set of entities contained within the current data source.
		/// </summary>
		/// <typeparam name="TEntity">The type of entity to retrieve a set of.</typeparam>
		/// <returns>A set of entities of the specified type that are contained within the current data source.</returns>
		IDbSet<TEntity> GetEntitySet<TEntity>()
			where TEntity : DomainObject;

		/// <summary>
		/// Retrieves a database reference from the current data source
		/// </summary>
		Database Database { get; }
	}
}