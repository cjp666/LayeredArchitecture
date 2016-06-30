using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace WindowsService
{
    public static class IPAddress
    {
        public static string Get()
        {
            var a = Dns.GetHostAddresses(Dns.GetHostName()).AsQueryable();
            var result = String.Empty;

            foreach (var addr in a)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    result = addr.ToString();
                }
            }
            return result;
        }
    }
}