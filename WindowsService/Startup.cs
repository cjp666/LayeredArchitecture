using Owin;
using System.Web.Http;

namespace WindowsService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            // http://www.codeproject.com/Articles/557232/Implementing-Custom-DelegatingHandler-in-ASP-NET-W
            // Add the compression handler
            config.MessageHandlers.Add(new CompressionDelegateHandler());

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(config);
        }
    }
}