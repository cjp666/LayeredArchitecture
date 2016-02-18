using System.Data.Entity;

namespace Fsh.Infrastructure
{
	/// <summary>
	/// Implements the <see cref="IDatabaseInitializer{TContext}"/> interface to prevent any custom initialization
	/// from being performed on the connected data source.
	/// </summary>
	public class NullDatabaseInitializer : IDatabaseInitializer<DbContext>
	{
		/// <summary>
		/// Executes the strategy to initialize the database for the given context.
		/// </summary>
		/// <param name="context">The current data source context instance.</param>
		public void InitializeDatabase(DbContext context)
		{
			// DO NOTHING!
		}
	}
}