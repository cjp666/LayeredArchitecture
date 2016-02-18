using System;

namespace CJSoftware.CrossCutting.Extensions
{
	/// <summary>
	/// Contains functionality to extend the <see cref="Type"/> class.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines whether the given type is a generic base of the type in question.
		/// </summary>
		/// <param name="type">The type to check for a generic base.</param>
		/// <param name="genericBaseType">The expected generic base type.</param>
		/// <returns>True if the type inherits the expected generic base type; false otherwise.</returns>
		/// <remarks>
		/// Note: This method circumvents the issue with <seealso cref="Type.IsAssignableFrom"/> whereby
		/// generic types are not counted unless they generic parameters are also known.
		/// </remarks>
		public static bool IsOrInheritsGeneric(this Type type, Type genericBaseType)
		{
			var currentType = type;

			// Walk up the inheritance chain, looking for the generic base
			while (currentType != null && (!currentType.IsGenericType || currentType.GetGenericTypeDefinition() != genericBaseType))
			{
				currentType = currentType.BaseType;
			}

			// If the current type is null, then nothing was found; otherwise we found a match
			return currentType != null;
		}
	}
}