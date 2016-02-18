using Autofac;
using CJSoftware.Domain.Repositories;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	///     A module describing the Infrastructure layer of the application
	/// </summary>
	public class InfrastructureModule : Module
	{
		/// <summary>
		///     Loads all registrations provided by the Infrastructure module
		/// </summary>
		/// <param name="builder">The current instance of the IoC Container Builder</param>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DatabaseContext>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			var assembly = typeof (InfrastructureModule).Assembly;

			builder.RegisterAssemblyTypes(assembly)
				.Where(t => !t.IsAbstract && t.IsAssignableTo<IRepository>())
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}