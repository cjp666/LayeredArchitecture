using Autofac;

namespace CJSoftware.CrossCutting.IoC
{
	/// <summary>
	/// A factory for handling registration of and access to the IoC container instance.
	/// </summary>
	public static class IoCFactory
	{
		private static ContainerBuilder _builder;

		/// <summary>
		/// Provides access to the current instance of the Autofac Container Builder.
		/// </summary>
		/// <returns>If no instance exists as yet, a new one will be created.</returns>
		public static ContainerBuilder Builder
		{
			get { return _builder ?? (_builder = new ContainerBuilder()); }
		}

		/// <summary>
		/// Provides access to the registered instance of the IoC container.
		/// </summary>
		/// <returns>This property will return a null reference until the
		/// <see cref="RegisterContainer"/> method is called.</returns>
		public static IContainer Container { get; private set; }

		/// <summary>
		/// Builds and registers the IoC container with the factory.
		/// </summary>
		/// <returns>An instance of the constructed IoC container.</returns>
		public static IContainer RegisterContainer()
		{
			Container = Builder.Build();

			return Container;
		}
	}
}