using log4net;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    partial class ExampleService : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ExampleService));

        private IDisposable _exampleService;
        private readonly string _baseAddress;

        public ExampleService()
        {
            InitializeComponent();

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

            _baseAddress = String.Format("http{0}://{1}:{2}/", useHTTPS ? "s" : String.Empty, ipAddress, port);
            _log.InfoFormat("Example Service is loading on {0}", _baseAddress);
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("Example Service API has started");

            _exampleService = WebApp.Start<Startup>(_baseAddress);
        }

        protected override void OnStop()
        {
            _log.Info("Example Service API has stopped");

            if (_exampleService != null)
            {
                _exampleService.Dispose();
            }
        }
    }
}