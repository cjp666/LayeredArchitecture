using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CJSoftware.CrossCutting.IoC;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using WebService.Providers;

[assembly: OwinStartup(typeof(WebService.Startup))]

namespace WebService
{
    public class Startup
    {
        private readonly Type ModuleAssemblyAttributeType = typeof(ModuleAssemblyAttribute);
        private readonly Type AutofacModuleType = typeof(IModule);

        public void Configuration(IAppBuilder app)
        {
            SetupDependencyInjection(app);

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var config = new HttpConfiguration();

            // config.MessageHandlers.Add(new CompressionDelegateHandler());

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            app.UseWebApi(config);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(IoCFactory.Container);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void SetupDependencyInjection(IAppBuilder app)
        {
            // Get the instances of all application modules and start up the kernel
            Kernel.Start(GetModuleAssemblies());

            // Register all controllers from this assembly and API controllers from the Distributed Services
            IoCFactory.Builder.RegisterControllers(typeof(ApiController).Assembly);
            IoCFactory.Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Finally build the container through the kernel
            Kernel.BuildContainer();

            // Set up the ASP.NET dependency resolution to use Autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoCFactory.Container));
            app.UseAutofacMiddleware(IoCFactory.Container);
        }

        private IModule[] GetModuleAssemblies()
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
            var assemblies =
                from file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                where !Path.GetFileName(file).Equals("WebServices.dll", StringComparison.InvariantCultureIgnoreCase)
                select Assembly.LoadFrom(file);

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