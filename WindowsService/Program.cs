using log4net;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace WindowsService
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

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
                //var servicesToRun = new ServiceBase[]
                //{
                //    new WTPAPIService()
                //};
                //ServiceBase.Run(servicesToRun);
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
    }
}