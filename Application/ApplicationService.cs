using CJSoftware.CrossCutting;
using CJSoftware.Domain;

namespace CJSoftware.Application
{
	/// <summary>
	///     Base-level implementation of a service responsible for controlling the
	///     flow of business logic within the application
	/// </summary>
	public abstract class ApplicationService : DisposableObject
	{
		private readonly IUnitOfWork _unitOfWork;

		/// <summary>
		///     Initializes a new instance of the <see cref="ApplicationService" /> class
		/// </summary>
		/// <param name="unitOfWork">The current unit of work instance providing application data access</param>
		protected ApplicationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		///     Provides access to the current unit of work instance providing application data access
		/// </summary>
		protected IUnitOfWork UnitOfWork
		{
			get { return _unitOfWork; }
		}

		/// <summary>
		///     Completes and persists all operations that have been performed under the current session
		/// </summary>
		protected void Save()
		{
			_unitOfWork.Complete();
		}
	}
}