#if FEATURE_LEGACY_REFLECTION_API
// ReSharper disable once CheckNamespace

using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>
	/// Contains static methods for retrieving custom attributes.
	/// This allows us to use the new Reflection API in .NET 4.5 back to .NET 4.0.
	/// Since this is the only way to access Reflection methods in .NET Core, we need to use it.
	/// </summary>
	internal static class CustomAttributeExtensions
	{
		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		/// <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)Attribute.GetCustomAttribute(element, typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified assembly. </summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="element" /> is null. </exception>
		public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>) element.GetCustomAttributes(typeof(T), false);
		}
	}
}
#endif
