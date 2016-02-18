using System.Collections.Generic;
using System.Web.Http;

namespace CJSoftware.WebServices.Controllers
{
	public class AboutController : ApiController
	{
		/// <summary>
		///     Get the version information
		/// </summary>
		/// <returns>A list of strings containing information about the application</returns>
		public IEnumerable<string> Get()
		{
			var aboutInfo = new List<string>
			{
				"Layered Architecture",
				"Version: 1.0.0",
				"18 Feb 2016"
			};

			return aboutInfo;
		}
	}
}