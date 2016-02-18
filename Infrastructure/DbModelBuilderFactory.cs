using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using CJSoftware.CrossCutting.Extensions;
using CJSoftware.Infrastructure.DatabaseMapping;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	/// A factory that handles the construction of a database model compiled from given type information.
	/// </summary>
	public static class DbModelBuilderFactory
	{
		private const string DEFAULT_CONFIGURATION_CONNECTION_NAME = "SimpleSQL";

		private static readonly object Locker = new object();
		private static readonly ConcurrentDictionary<string, DbCompiledModel> CompiledModels
			= new ConcurrentDictionary<string, DbCompiledModel>();

		/// <summary>
		/// Gets a constructed database model compiled from the given type information.
		/// </summary>
		/// <param name="typeContainingModels">The type information containing the models for construction.</param>
		/// <returns>A constructed database model compiled from the given information.</returns>
		/// <returns>Any constructed model will be cached under the full name of the given type.</returns>
		public static DbCompiledModel GetModel(Type typeContainingModels)
		{
			lock (Locker)
			{
				DbCompiledModel compiledModel;
				CompiledModels.TryGetValue(typeContainingModels.FullName, out compiledModel);

				if (compiledModel == null)
				{
					using (var connection = DbConnectionResolver.CreateConnectionFromConfig(DEFAULT_CONFIGURATION_CONNECTION_NAME))
					{
						connection.Open();

						var modelBuilder = new DbModelBuilder();

						// Remove all the default EF mapping conventions
						// Remove all the EF conventions
						modelBuilder.Conventions.Remove<AssociationInverseDiscoveryConvention>();
						//modelBuilder.Conventions.Remove<AttributeToColumnAnnotationConvention>();
						//modelBuilder.Conventions.Remove<AttributeToTableAnnotationConvention>();
						modelBuilder.Conventions.Remove<ColumnAttributeConvention>();
						modelBuilder.Conventions.Remove<ColumnOrderingConvention>();
						modelBuilder.Conventions.Remove<ColumnOrderingConventionStrict>();
						modelBuilder.Conventions.Remove<ComplexTypeAttributeConvention>();
						modelBuilder.Conventions.Remove<ComplexTypeDiscoveryConvention>();
						modelBuilder.Conventions.Remove<ConcurrencyCheckAttributeConvention>();
						modelBuilder.Conventions.Remove<Convention>();
						modelBuilder.Conventions.Remove<DatabaseGeneratedAttributeConvention>();
						modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
						modelBuilder.Conventions.Remove<DeclaredPropertyOrderingConvention>();
						modelBuilder.Conventions.Remove<ForeignKeyAssociationMultiplicityConvention>();
						modelBuilder.Conventions.Remove<ForeignKeyDiscoveryConvention>();
						//modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
						modelBuilder.Conventions.Remove<ForeignKeyNavigationPropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<ForeignKeyPrimitivePropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<IdKeyDiscoveryConvention>();
						//modelBuilder.Conventions.Remove<IndexAttributeConvention>();
						modelBuilder.Conventions.Remove<InversePropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<KeyAttributeConvention>();
						modelBuilder.Conventions.Remove<KeyDiscoveryConvention>();
						modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
						modelBuilder.Conventions.Remove<MappingInheritedPropertiesSupportConvention>();
						modelBuilder.Conventions.Remove<MaxLengthAttributeConvention>();
						modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
						modelBuilder.Conventions.Remove<NotMappedPropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<NotMappedTypeAttributeConvention>();
						modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
						modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
						modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
						modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
						modelBuilder.Conventions.Remove<PrimaryKeyNameForeignKeyDiscoveryConvention>();
						//modelBuilder.Conventions.Remove<PrimitivePropertyAttributeConfigurationConvention>();
						//modelBuilder.Conventions.Remove<PropertyAttributeConfigurationConvention>();
						modelBuilder.Conventions.Remove<PropertyMaxLengthConvention>();
						modelBuilder.Conventions.Remove<RequiredNavigationPropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<RequiredPrimitivePropertyAttributeConvention>();
						modelBuilder.Conventions.Remove<SqlCePropertyMaxLengthConvention>();
						modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();
						modelBuilder.Conventions.Remove<StringLengthAttributeConvention>();
						modelBuilder.Conventions.Remove<TableAttributeConvention>();
						modelBuilder.Conventions.Remove<TimestampAttributeConvention>();
						//modelBuilder.Conventions.Remove<TypeAttributeConfigurationConvention>();
						modelBuilder.Conventions.Remove<TypeNameForeignKeyDiscoveryConvention>();

						// Use reflection to discover all of the EntityTypeConfiguration(s) and ComplexTypeConfiguration(s)
						AddEntityTypeConfigurations(modelBuilder, typeContainingModels.Assembly);

						var model = modelBuilder.Build(connection);
						compiledModel = model.Compile();

						if (compiledModel != null)
						{
							CompiledModels.TryAdd(typeContainingModels.FullName, compiledModel);
						}
					}
				}

				return compiledModel;
			}
		}

		private static void AddEntityTypeConfigurations(DbModelBuilder modelBuilder, Assembly assembly)
		{
			foreach (var type in assembly.GetTypes().OrderBy(x => x.FullName))
			{
				AddEntityTypeConfigurations(modelBuilder, type);
			}
		}

		private static void AddEntityTypeConfigurations(DbModelBuilder modelBuilder, Type type)
		{
			// If we have an abstract type, or it is not a configuration map, skip it
			if (type.IsAbstract || !type.IsOrInheritsGeneric(typeof(StructuralTypeConfiguration<>)))
			{
				return;
			}

			// Build the instance and add to the model builder
			var instance = (dynamic)Activator.CreateInstance(type);
			if (type.IsOrInheritsGeneric(typeof(EntityTypeConfigurationBase<>)))
			{
				instance.Map();
			}

			modelBuilder.Configurations.Add(instance);
		}
	}
}