using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CJSoftware.CrossCutting.IoC;
using Newtonsoft.Json.Serialization;

namespace CJSoftware.WebServices
{
	public class WebApiApplication : HttpApplication
	{
		private static readonly Type ModuleAssemblyAttributeType = typeof(ModuleAssemblyAttribute);
		private static readonly Type AutofacModuleType = typeof(IModule);

		protected void Application_Start()
		{
			SetupDependencyInjection();

			AreaRegistration.RegisterAllAreas();

			GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Add the compression handler
			// http://www.codeproject.com/Articles/557232/Implementing-Custom-DelegatingHandler-in-ASP-NET-W
			GlobalConfiguration.Configuration.MessageHandlers.Add(new CompressionDelegateHandler());
		}

		private static void SetupDependencyInjection()
		{
			// Get the instances of all application modules and start up the kernel
			Kernel.Start(GetModuleAssemblies());

			// Register all controllers from this assembly and API controllers from the Distributed Services
			IoCFactory.Builder.RegisterControllers(typeof(WebApiApplication).Assembly);
			// IoCFactory.Builder.RegisterApiControllers(typeof(DistributedServicesModule).Assembly);

			// Finally build the container through the kernel
			Kernel.BuildContainer();

			// Set up the ASP.NET dependency resolution to use Autofac
			DependencyResolver.SetResolver(new AutofacDependencyResolver(IoCFactory.Container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IoCFactory.Container);
		}

		private static IModule[] GetModuleAssemblies()
		{
			var moduleTypes = new List<Type>();

			// Scan all referenced assemblies loaded for module assemblies
			// Note: We were previously using AppDomain.GetAssemblies(), but this causes an issue with optimization in that
			//		 the majority of our assemblies will not be re-loaded into the AppDomain after it has been recycled.
			//		 This ultimately caused issues when attempting dependency injection of types contained within these
			//		 unloaded assemblies.
			//
			//		 For more information on this, refer to these StackOverflow posts:
			//		 Issue Resolved:
			//			http://stackoverflow.com/q/15740726/308012
			//
			//		 Explanation of AppDomain.GetAssemblies() vs BuildManager.GetReferencedAssemblies():
			//			http://stackoverflow.com/a/2479400/308012
			var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

			foreach (var assembly in assemblies)
			{
				var moduleAssemblyAttributes = (ModuleAssemblyAttribute[])assembly.GetCustomAttributes(ModuleAssemblyAttributeType, inherit: false);

				if (moduleAssemblyAttributes.Length == 0)
				{
					continue;
				}

				// We have hold of a module assembly, so scan it for all module types
				var modules = assembly.GetTypes().Where(type => AutofacModuleType.IsAssignableFrom(type));
				moduleTypes.AddRange(modules);
			}

			// Instantiate all of the collected modules
			var moduleInstances = new IModule[moduleTypes.Count];
			for (int i = 0; i < moduleTypes.Count; i++)
			{
				moduleInstances[i] = (IModule)Activator.CreateInstance(moduleTypes[i]);
			}

			return moduleInstances;
		}
}
}
