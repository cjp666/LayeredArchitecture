using System.Transactions;
using CJSoftware.CrossCutting;
using CJSoftware.Domain;

namespace CJSoftware.Infrastructure
{
	/// <summary>
	/// Wraps the system <see cref="TransactionScope"/>to provide a transactional scope within
	///  which persisting operations can be safely executed.
	/// </summary>
	public class DefaultTransactionScope : DisposableObject, ITransactionScope
	{
		private readonly TransactionScope _internalScope;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultTransactionScope"/>.
		/// </summary>
		public DefaultTransactionScope()
		{
			_internalScope = new TransactionScope();
		}

		/// <summary>
		/// Abandons the current transaction scope and any operations executed within it.
		/// </summary>
		public void Abandon()
		{
			Dispose();
		}

		/// <summary>
		/// Attempts to finish the transaction scope and allow persistence operations to be completed.
		/// </summary>
		public void Finish()
		{
			_internalScope.Complete();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">True if the object's Dispose method has been explicitly called; false if the call
		/// has come through the object's finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_internalScope.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}