using System;
using CJSoftware.Domain.Model;

namespace CJSoftware.Domain
{
	/// <summary>
	/// Provides a contract for a logical unit of work to control data access within an application.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Registers the given domain object with the unit of work as a new entity.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		void RegisterNew(IDomainObject domainObject);

		/// <summary>
		/// Registers the given domain object with the unit of work as a deleted entity.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		void RegisterDeleted(IDomainObject domainObject);

		/// <summary>
		/// Registers the given domain object with the unit of work as an entity that has been changed.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		void RegisterUpdated(IDomainObject domainObject);

		/// <summary>
		/// Flushes out the any cached copy of the given domain object and re-gets it from the data context.
		/// </summary>
		/// <param name="domainObject">The domain object to be reloaded.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		void Reload(IDomainObject domainObject);

		/// <summary>
		/// Persists any modifications or transactions made within the current data context scope.
		/// </summary>
		void Complete();

		/// <summary>
		/// Creates and starts a new transactional scope within which persisting operations can be safely executed.
		/// </summary>
		/// <returns>A new transaction scope instance.</returns>
		ITransactionScope BeginTransaction();
	}
}