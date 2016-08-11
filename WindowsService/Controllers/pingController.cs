using System.Web.Http;

namespace WindowsService.Controllers
{
    public class PingController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("pong");
        }
    }
}
