using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
        }
    }
}