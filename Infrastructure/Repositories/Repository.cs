using System;
using System.Collections.Generic;
using System.Linq;
using CJSoftware.CrossCutting;
using CJSoftware.Domain;
using CJSoftware.Domain.Model;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Infrastructure.Repositories
{
	/// <summary>
	///     Provides an abstract implementation of the <see cref="IRepository" /> interface.
	/// </summary>
	/// <typeparam name="TKey">The type of the unique identifier for the entity.</typeparam>
	/// <typeparam name="TEntity">
	///     The type of the entity for which the repository handles operations.
	///     This must inherit from the <see cref="DomainObject" /> class.
	/// </typeparam>
	public abstract class Repository<TKey, TEntity> : DisposableObject, IRepository<TKey, TEntity>
		where TEntity : DomainObject
	{
		private readonly IUnitOfWork _unitOfWork;

		/// <summary>
		///     Initializes a new instance of the <see cref="Repository{TKey, TEntity}" /> base class.
		/// </summary>
		/// <param name="unitOfWork">The current application instance of the logical unit of work.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given unit of work is null.</exception>
		protected Repository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		///     The current unit of work with which this repository is interacting.
		/// </summary>
		public IUnitOfWork UnitOfWork
		{
			get { return _unitOfWork; }
		}

		/// <summary>
		///     Creates and starts a new transactional scope within which persisting operations can be safely executed.
		/// </summary>
		/// <returns>A new transaction scope instance.</returns>
		public ITransactionScope BeginTransaction()
		{
			return _unitOfWork.BeginTransaction();
		}

		/// <summary>
		///     Retrieves all instances of the entity handled by the repository.
		/// </summary>
		/// <param name="includes">An optional collection of known entity information to be included.</param>
		/// <returns>A collection of all instances of the entity.</returns>
		public IEnumerable<TEntity> GetAll(params string[] includes)
		{
			return Query(includes).AsEnumerable();
		}

		/// <summary>
		///     Retrieves a queryable connection to the instances of the known entity.
		/// </summary>
		/// <param name="includes">An optional collection of known entity information to be included.</param>
		/// <returns>A queryable connection to all instances of the entity.</returns>
		public abstract IQueryable<TEntity> Query(params string[] includes);

		/// <summary>
		///     Retrieves an instance of the known entity with a matching unique identifier value.
		/// </summary>
		/// <param name="id">The unique identifier value to match to an entity instance.</param>
		/// <returns>An instance of the matched entity if one is found; otherwise a null value.</returns>
		public abstract TEntity GetById(TKey id);

		/// <summary>
		///     Adds the given entity instance to the repository.
		/// </summary>
		/// <param name="entity">The entity instance to be added.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		public void Add(TEntity entity)
		{
			_unitOfWork.RegisterNew(entity);
		}

		/// <summary>
		///     Deletes the given entity instance and removes it from the repository.
		/// </summary>
		/// <param name="entity">The entity instance to be deleted.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		public void Delete(TEntity entity)
		{
			_unitOfWork.RegisterDeleted(entity);
		}

		/// <summary>
		///     Updates the existing instance of the given entity accessible to the repository.
		/// </summary>
		/// <param name="entity">An updated version of an existing entity.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		public void Modify(TEntity entity)
		{
			_unitOfWork.RegisterUpdated(entity);
		}

		/// <summary>
		///     Similar to <see cref="Modify" /> but without actually doing anything
		/// </summary>
		/// <param name="entity">An updated version of an existing entity</param>
		/// <remarks>Really just so that mock knows when an entity is updated</remarks>
		public void Update(TEntity entity)
		{
		}

		/// <summary>
		///     Clears any cached information pertaining to the given entity and reloads the entity instance.
		/// </summary>
		/// <param name="entity">The entity for which information should be reloaded.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given entity is a null reference.</exception>
		public void Reload(TEntity entity)
		{
			_unitOfWork.Reload(entity);
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">
		///     True if the object's Dispose method has been explicitly called; false if the call
		///     has come through the object's finalizer.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_unitOfWork.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}