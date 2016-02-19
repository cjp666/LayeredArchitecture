using System;
using System.Diagnostics.CodeAnalysis;

namespace CJSoftware.Application.Core
{
	/// <summary>
	///     Implementation of ISystemClock
	/// </summary>
	/// <remarks>
	///     Excluded from code coverage as we can't write tests for this (well that I know how to)
	/// </remarks>
	[ExcludeFromCodeCoverage]
	public class SystemClock : ISystemClock
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}