using System;
using System.Configuration;
using System.Data.Common;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	/// Handles resolution of a database connection from a configuration file.
	/// </summary>
	public static class DbConnectionResolver
	{
		/// <summary>
		/// Creates a database connection using a connection string found within the current configuration file
		///  that is registered under the given connection name.
		/// </summary>
		/// <param name="connectionName">The name of the connection string registration in the configuration file.</param>
		/// <returns>A database connection set up with the resolved connection string.</returns>
		/// <exception cref="ConfigurationErrorsException">Thrown when there is a problem with resolving or
		/// creating the database connection.</exception>
		public static DbConnection CreateConnectionFromConfig(string connectionName)
		{
			string providerName;
			string connectionString;

			var connectionStrings = ConfigurationManager.ConnectionStrings[connectionName];
			if (connectionStrings == null)
			{
				var server = ConfigurationManager.AppSettings["server"];
				var database = ConfigurationManager.AppSettings["database"];
				var security = ConfigurationManager.AppSettings["security"];
				var sqlAuthentication = String.Compare(security, "NT", StringComparison.InvariantCultureIgnoreCase) != 0;
				var sqlUsername = ConfigurationManager.AppSettings["sqlUsername"] ?? String.Empty;
				var sqlPassword = ConfigurationManager.AppSettings["sqlPassword"] ?? String.Empty;

				connectionString = String.Format("Data Source={0};Initial Catalog={1};Trusted_Connection={2};MultipleActiveResultSets=True;", server, database, !sqlAuthentication);
				if (sqlAuthentication)
				{
					connectionString = String.Format("{0}User Id={1};Password={2};", connectionString, sqlUsername, sqlPassword);
				}

				// throw new ConfigurationErrorsException("Invalid connection name \"" + connectionName + "\"");

				providerName = "System.Data.SqlClient";
			}
			else
			{
				providerName = connectionStrings.ProviderName;
				connectionString = connectionStrings.ConnectionString;
			}

			//Get the factory for the given provider (e.g. "System.Data.SqlClient")
			var factory = DbProviderFactories.GetFactory(providerName);
			if (factory == null)
			{
				throw new ConfigurationErrorsException("Could not obtain factory for provider \"" + providerName + "\"");
			}

			var connection = factory.CreateConnection();
			if (connection == null)
			{
				throw new ConfigurationErrorsException("Could not obtain connection from factory");
			}

			connection.ConnectionString = connectionString;

			return connection;
		}
	}
}