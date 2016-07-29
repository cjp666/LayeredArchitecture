using System.Web.Http;

namespace WindowsService.Controllers
{
    public class PingController : ApiController
    {
        public string Get()
        {
            return "pong";
        }
    }
}
