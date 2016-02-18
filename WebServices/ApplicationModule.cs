using Autofac;

namespace CJSoftware.WebServices
{
	public class ApplicationModule : Module
	{
		/// <summary>
		///     Loads all registrations provided by the Application module.
		/// </summary>
		/// <param name="builder">The current instance of the IoC Container Builder.</param>
		protected override void Load(ContainerBuilder builder)
		{
			// var assembly = typeof (ApplicationModule).Assembly;
		}
	}
}