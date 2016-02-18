using Autofac;
using Autofac.Core;

namespace CJSoftware.CrossCutting.IoC
{
	/// <summary>
	/// Represents the kernel of an application that supports dependency injection and makes use
	/// of the <see cref="IoCFactory"/> and registered containers.
	/// </summary>
	public static class Kernel
	{
		/// <summary>
		/// Triggers the start of the application kernel by registering all of the given modules.
		/// </summary>
		public static void Start(params IModule[] modules)
		{
			foreach (var module in modules)
			{
				IoCFactory.Builder.RegisterModule(module);
			}
		}

		/// <summary>
		/// Ensures that the IoC container has been build and registered with the <see cref="IoCFactory"/>.
		/// </summary>
		/// <returns>The registered instance of the IoC container.</returns>
		public static IContainer BuildContainer()
		{
			return IoCFactory.RegisterContainer();
		}
	}
}