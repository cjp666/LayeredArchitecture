using System.Web.Http;

namespace WebService.Controllers
{
    public class PingController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("pong");
        }
    }
}
