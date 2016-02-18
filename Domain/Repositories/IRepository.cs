using System;
using System.Collections.Generic;

namespace CJSoftware.Domain.Repositories
{
	public interface IRepository
	{
		/// <summary>
		///     The current unit of work with which this repository is interacting.
		/// </summary>
		IUnitOfWork UnitOfWork { get; }

		/// <summary>
		///     Creates and starts a new transactional scope within which persisting operations can be safely executed.
		/// </summary>
		/// <returns>A new transaction scope instance.</returns>
		ITransactionScope BeginTransaction();

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>
		///     Do not delete this; it might look redundant, but it allows repositories retrieved from IoC using their
		///     interface definitions to be disposed. As the concrete repository implementations inherit DisposableObject
		///     and are IDisposable adding this method here keeps FxCop and ReSharper happy.
		///     See http://msdn.microsoft.com/query/dev11.query?appId=Dev11IDEF1&l=EN-US&k=k(CA1063)&rd=true
		/// </remarks>
		void Dispose();
	}

	/// <summary>
	///     Provides a contract for a repository that allows operations to be executed against a specific entity.
	/// </summary>
	/// <typeparam name="TKey">The type of the unique identifier for the entity.</typeparam>
	/// <typeparam name="TEntity">The type of the entity for which the repository handles operations.</typeparam>
	public interface IRepository<in TKey, TEntity> : IRepository
		where TEntity : class
	{
		/// <summary>
		///     Retrieves all instances of the entity handled by the repository.
		/// </summary>
		/// <param name="includes">An optional collection of known entity information to be included.</param>
		/// <returns>A collection of all instances of the entity.</returns>
		IEnumerable<TEntity> GetAll(params string[] includes);

		/// <summary>
		///     Retrieves an instance of the known entity with a matching unique identifier value.
		/// </summary>
		/// <param name="id">The unique identifier value to match to an entity instance.</param>
		/// <returns>An instance of the matched entity if one is found; otherwise a null value.</returns>
		TEntity GetById(TKey id);

		/// <summary>
		///     Adds the given entity instance to the repository.
		/// </summary>
		/// <param name="entity">The entity instance to be added.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		void Add(TEntity entity);

		/// <summary>
		///     Deletes the given entity instance and removes it from the repository.
		/// </summary>
		/// <param name="entity">The entity instance to be deleted.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		void Delete(TEntity entity);

		/// <summary>
		///     Updates the existing instance of the given entity accessible to the repository.
		/// </summary>
		/// <param name="entity">An updated version of an existing entity.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		void Modify(TEntity entity);

		/// <summary>
		///     Clears any cached information pertaining to the given entity and reloads the entity instance.
		/// </summary>
		/// <param name="entity">The entity for which information should be reloaded.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		void Reload(TEntity entity);

		/// <summary>
		///     Similar to <see cref="Modify" /> but without actually doing anything
		/// </summary>
		/// <param name="entity">An updated version of an existing entity</param>
		/// <remarks>Really just so that mock knows when an entity is updated</remarks>
		void Update(TEntity entity);
	}
}