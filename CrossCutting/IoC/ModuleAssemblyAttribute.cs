using System;

namespace CJSoftware.CrossCutting.IoC
{
	/// <summary>
	/// Represents an assembly that contains a module for IoC registration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ModuleAssemblyAttribute : Attribute
	{
	}
}