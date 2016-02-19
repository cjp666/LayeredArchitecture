using System;

namespace CJSoftware.Application.Core
{
	/// <summary>
	///     System Clock rather than using DateTime.Now so that
	///     for coded unit tests this can be mocked to supply a
	///     known date and time
	/// </summary>
	/// <remarks>
	///     The concept for this came from this blog post
	///     http://www.jerriepelser.com/blog/unit-testing-with-dates
	/// </remarks>
	public interface ISystemClock
	{
		/// <summary>
		///     Retrieve the current date/time
		/// </summary>
		/// <returns>The current date/time</returns>
		DateTime GetCurrentDateTime();
	}
}