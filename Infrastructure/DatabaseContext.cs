using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CJSoftware.Domain;
using CJSoftware.Domain.Model;
using Fsh.Infrastructure;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	///     Interfaces with the Entity Framework database connection through implementing the application unit of work.
	/// </summary>
	public sealed class DatabaseContext : DbContext, IDbSetUnitOfWork
	{
		// ReSharper disable once InconsistentNaming
		private const string DATABASE_CONTEXT_DATASOURCE_NAME = "Name=SimpleSQL";

		private ITransactionScope _currentTransactionScope;

		/// <summary>
		///     Initializes a new instance of the <see cref="DatabaseContext" /> class.
		/// </summary>
		public DatabaseContext()
			: base(DATABASE_CONTEXT_DATASOURCE_NAME, DbModelBuilderFactory.GetModel(typeof (DatabaseContext)))
		{
			Database.SetInitializer<DatabaseContext>(new NullDatabaseInitializer());
			Database.CommandTimeout = 180;
		}

		public DatabaseContext(string connectionString)
			: base(connectionString, DbModelBuilderFactory.GetModel(typeof(DatabaseContext)))
		{
			Database.SetInitializer<DatabaseContext>(new NullDatabaseInitializer());
			Database.CommandTimeout = 180;
		}

		/// <summary>
		///     Registers the given domain object with the unit of work as a new entity.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		public void RegisterNew(IDomainObject domainObject)
		{
			var type = ObjectContext.GetObjectType(domainObject.GetType());
			Set(type).Add(domainObject);
		}

		/// <summary>
		///     Registers the given domain object with the unit of work as a deleted entity.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		public void RegisterDeleted(IDomainObject domainObject)
		{
			var type = ObjectContext.GetObjectType(domainObject.GetType());
			Set(type).Remove(domainObject);

			// See http://stackoverflow.com/questions/9201421/ef-4-1-many-to-many-delete-parent-should-delete-related-children
			// that's why we commented following two lines and used the Remove function . Entry(item).State = EntityState.Deleted does not delete child objects.
			//Set(type).Add(item);
			//Entry(item).State = EntityState.Deleted;
		}

		/// <summary>
		///     Registers the given domain object with the unit of work as an entity that has been changed.
		/// </summary>
		/// <param name="domainObject">The domain object to be registered.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		public void RegisterUpdated(IDomainObject domainObject)
		{
			var type = ObjectContext.GetObjectType(domainObject.GetType());

			Set(type).Add(domainObject);
			Entry(domainObject).State = EntityState.Modified;
		}

		/// <summary>
		///     Flushes out the any cached copy of the given domain object and re-gets it from the data context.
		/// </summary>
		/// <param name="domainObject">The domain object to be reloaded.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given domain object is null.</exception>
		public void Reload(IDomainObject domainObject)
		{
			var addedItems = ChangeTracker.Entries()
				.Where(x => x.State == EntityState.Added);
			if (!(addedItems.Any(x => (IDomainObject)x.Entity == domainObject)))
			{
				Entry(domainObject).Reload();
			}
		}

		/// <summary>
		///     Persists any modifications or transactions made within the current data context scope.
		/// </summary>
		public void Complete()
		{
			// TODO Auditing
			// var audit = new AuditService();
			// audit.Audit(ChangeTracker);

			var retry = 0;

			do
			{
				try
				{
					SaveChanges();
					break;
				}
				catch (DbUpdateException dbEx)
				{
					if (dbEx.InnerException != null && dbEx.InnerException.InnerException != null)
					{
						var message = dbEx.InnerException.InnerException.Message;
						if (message.Contains("Violation of PRIMARY KEY constraint"))
						{
							throw;
						}
					}

					if (++retry > 25)
					{
						Debug.WriteLine(dbEx.Message);
						throw;
					}
					var rnd = new Random();
					var sleep = rnd.Next(250, 750);
					Thread.Sleep(sleep);
				}
				catch (System.Data.Entity.Validation.DbEntityValidationException ex)
				{
					Debug.WriteLine(ex.Message);
					foreach (var a in ex.EntityValidationErrors)
					{
						foreach (var b in a.ValidationErrors)
						{
							Debug.WriteLine(b.ErrorMessage);
						}
					}
					throw;
				}
			} while (true);
		}

		/// <summary>
		///     Creates and starts a new transactional scope within which persisting operations can be safely executed.
		/// </summary>
		/// <returns>A new transaction scope instance.</returns>
		public ITransactionScope BeginTransaction()
		{
			_currentTransactionScope = new DefaultTransactionScope();

			return _currentTransactionScope;
		}

		/// <summary>
		///     Retrieves a database set of entities contained within the current data source.
		/// </summary>
		/// <typeparam name="TEntity">The type of entity to retrieve a set of.</typeparam>
		/// <returns>A set of entities of the specified type that are contained within the current data source.</returns>
		IDbSet<TEntity> IDbSetUnitOfWork.GetEntitySet<TEntity>()
		{
			return Set<TEntity>();
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
			if (disposing && _currentTransactionScope != null)
			{
				_currentTransactionScope.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}