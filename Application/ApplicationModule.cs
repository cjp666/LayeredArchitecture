using System.Diagnostics.CodeAnalysis;
using Autofac;
using CJSoftware.Application.Core;
using CJSoftware.Application.Translators;

namespace CJSoftware.Application
{
	/// <summary>
	///     A module describing the Application layer of the application.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class ApplicationModule : Module
	{
		/// <summary>
		///     Loads all registrations provided by the Application module.
		/// </summary>
		/// <param name="builder">The current instance of the IoC Container Builder.</param>
		protected override void Load(ContainerBuilder builder)
		{
			var assembly = typeof(ApplicationModule).Assembly;

			// Register all shell application services
			builder.RegisterAssemblyTypes(assembly)
				.Where(t => !t.IsAbstract && t.IsAssignableTo<ApplicationService>())
				.AsSelf()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.IsAssignableTo<ITranslator>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
				.Where(t => !t.IsAbstract && t.IsAssignableTo<ISystemClock>())
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}