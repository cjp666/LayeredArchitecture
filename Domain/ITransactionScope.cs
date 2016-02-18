using System;

namespace CJSoftware.Domain
{
	/// <summary>
	/// Provides a contract to a transactional scope within which persisting operations can be safely executed.
	/// </summary>
	public interface ITransactionScope : IDisposable
	{
		/// <summary>
		/// Abandons the current transaction scope and any operations executed within it.
		/// </summary>
		void Abandon();

		/// <summary>
		/// Attempts to finish the transaction scope and allow persistence operations to be completed.
		/// </summary>
		void Finish();
	}
}