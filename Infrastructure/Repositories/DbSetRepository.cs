using System;
using System.Linq;
using CJSoftware.Domain.Model;

namespace CJSoftware.Infrastructure.Repositories
{
	/// <summary>
	/// Provides an abstract extension of the <see cref="Repository{TKey, TEntity}"/> class with database
	/// and domain-specific operations.
	/// </summary>
	/// <typeparam name="TKey">The type of the unique identifier for the entity.</typeparam>
	/// <typeparam name="TEntity">The type of the entity for which the repository handles operations.
	/// This must inherit from the <see cref="DomainObject"/> class and have a parameterless constructor.</typeparam>
	public abstract class DbSetRepository<TKey, TEntity> : Repository<TKey, TEntity>
		where TEntity : DomainObject, new()
	{
		private readonly IDbSetUnitOfWork _unitOfWork;

		/// <summary>
		/// Initializes a new instance of the <see cref="DbSetRepository{TKey, TEntity}"/> base class.
		/// </summary>
		/// <param name="unitOfWork">The current application instance of the logical unit of work.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given unit of work is null.</exception>
		protected DbSetRepository(IDbSetUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// The current database-specific unit of work with which this repository is interacting.
		/// </summary>
		public new IDbSetUnitOfWork UnitOfWork
		{
			get { return _unitOfWork; }
		}

		/// <summary>
		/// Retrieves a queryable connection to the instances of the known entity.
		/// </summary>
		/// <param name="includes">An optional collection of known entity information to be included.</param>
		/// <returns>A queryable connection to all instances of the entity.</returns>
		public override IQueryable<TEntity> Query(params string[] includes)
		{
			return _unitOfWork.GetEntitySet<TEntity>();
		}

		/// <summary>
		/// Retrieves an instance of the known entity with a matching unique identifier value.
		/// </summary>
		/// <param name="id">The unique identifier value to match to an entity instance.</param>
		/// <returns>An instance of the matched entity if one is found; otherwise a null value.</returns>
		public override TEntity GetById(TKey id)
		{
			var entity = _unitOfWork.GetEntitySet<TEntity>().Find(id);

			if (entity != null)
			{
				_unitOfWork.Reload(entity);
			}

			return entity;
		}
	}
}