using System;
using System.Data.Entity.ModelConfiguration;

namespace CJSoftware.Infrastructure.DatabaseMapping
{
	/// <summary>
	/// Provides a base for entity configurations.
	/// </summary>
	/// <typeparam name="T">The type of the entity object.</typeparam>
	public abstract class EntityTypeConfigurationBase<T> : EntityTypeConfiguration<T>
		where T : class
	{
		private readonly string _tableName;

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityTypeConfigurationBase{T}"/> class.
		/// </summary>
		/// <param name="tableName">The name of the table to map the entity to.</param>
		/// <exception cref="System.ArgumentException">Thrown when the given table name is not a valid string.</exception>
		protected EntityTypeConfigurationBase(string tableName)
		{
			// Store the table name to prevent it from being changed
			_tableName = !String.IsNullOrWhiteSpace(tableName) ? tableName : typeof(T).Name;
		}

		/// <summary>
		/// Performs the structured mapping of the properties and relationships of the entity
		/// to the relevant table.
		/// </summary>
		public void Map()
		{
			MapKeys();
			MapProperties();
			MapRelationships();
			MapIgnores();

			// Finish by mapping to the table
			ToTable(_tableName);
		}

		/// <summary>
		/// Maps the keys of the entity to the relevant columns in the associated table.
		/// </summary>
		/// <remarks>
		/// This method can be overridden by derived types to allow record-specific mapping.
		/// </remarks>
		protected virtual void MapKeys()
		{
		}

		/// <summary>
		/// Maps any properties that should be ignored to be so.
		/// </summary>
		/// <remarks>
		/// This method can be overridden by derived types to allow record-specific mapping.
		/// </remarks>
		protected virtual void MapIgnores()
		{
		}

		/// <summary>
		/// Maps the properties of the entity to the relevant columns in the associated table.
		/// </summary>
		/// <remarks>
		/// This method can be overridden by derived types to allow record-specific mapping.
		/// </remarks>
		protected virtual void MapProperties()
		{
		}

		/// <summary>
		/// Maps the known relationships between entities and tables.
		/// </summary>
		/// <remarks>
		/// This method can be overridden by derived types to allow record-specific mapping.
		/// </remarks>
		protected virtual void MapRelationships()
		{
		}
	}
}