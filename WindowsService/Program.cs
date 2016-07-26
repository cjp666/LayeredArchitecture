using log4net;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceProcess;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CJSoftware.CrossCutting.IoC;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Compilation;
using System.Linq;
using CJSoftware.WebServices;

namespace WindowsService
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        private static readonly Type ModuleAssemblyAttributeType = typeof(ModuleAssemblyAttribute);
        private static readonly Type AutofacModuleType = typeof(IModule);

        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var asConsole = String.Compare(ConfigurationManager.AppSettings["asConsole"], "true", StringComparison.CurrentCultureIgnoreCase) == 0;

            if (asConsole)
            {
                AsConsoleApplication();
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                {
                    new ExampleService()
                };
                ServiceBase.Run(servicesToRun);
            }
        }

        private static void AsConsoleApplication()
        {
            _log.Debug("Loading Example WebAPI as a console application");

            var ipAddress = ConfigurationManager.AppSettings["useIP"];
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                ipAddress = IPAddress.Get();
            }

            var port = ConfigurationManager.AppSettings["port"];
            if (String.IsNullOrWhiteSpace(port))
            {
                port = "9021";
            }

            var useHTTPS = String.Compare(ConfigurationManager.AppSettings["useHTTPS"], "true", StringComparison.CurrentCultureIgnoreCase) == 0;
            var useLocalhost = String.Compare(ConfigurationManager.AppSettings["useLocalhost"], "true", StringComparison.CurrentCultureIgnoreCase) == 0;

            if (useLocalhost)
            {
                ipAddress = "localhost";
            }

            var baseAddress = String.Format("http{0}://{1}:{2}/", useHTTPS ? "s" : String.Empty, ipAddress, port);
            _log.InfoFormat("Loading on {0}", baseAddress);

            using (WebApp.Start<Startup>(baseAddress))
            {
                _log.InfoFormat("WindowsService is listening on {0}", baseAddress);

                Console.WriteLine("");
                Console.WriteLine("WindowsService is listening on {0}", baseAddress);
                Console.WriteLine("Press ENTER to terminate...");
                Console.ReadLine();
            }
        }

        private static void SetupDependencyInjection()
        {
            // Get the instances of all application modules and start up the kernel
            Kernel.Start(GetModuleAssemblies());

            // Register all controllers from this assembly and API controllers from the Distributed Services
            IoCFactory.Builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            IoCFactory.Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Finally build the container through the kernel
            Kernel.BuildContainer();

            // Set up the ASP.NET dependency resolution to use Autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoCFactory.Container));
            // TODO remove GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IoCFactory.Container);
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
                var moduleAssemblyAttributes =
                    (ModuleAssemblyAttribute[])assembly.GetCustomAttributes(ModuleAssemblyAttributeType, false);

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
            for (var i = 0; i < moduleTypes.Count; i++)
            {
                moduleInstances[i] = (IModule)Activator.CreateInstance(moduleTypes[i]);
            }

            return moduleInstances;
        }
    }
}