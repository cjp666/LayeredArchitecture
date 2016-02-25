namespace CJSoftware.Domain.Model
{
	public class Company : DomainObject<int>
	{
		/// <summary>
		///		Unique code for the company
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		///		The company name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		The companies e-mail address
		/// </summary>
		public string EmailAddress { get; set; }
	}
}