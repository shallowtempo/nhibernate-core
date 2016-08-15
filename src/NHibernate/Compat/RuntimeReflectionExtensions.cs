#if FEATURE_LEGACY_REFLECTION_API
// ReSharper disable once CheckNamespace
namespace System.Reflection
{
	/// <summary>
	/// Provides methods that retrieve information about types at run time.
	/// This allows us to use the new Reflection API in .NET 4.5 back to .NET 4.0.
	/// Since this is the only way to access Reflection methods in .NET Core, we need to use it.
	/// </summary>
	internal static class RuntimeReflectionExtensions
	{
		public static InterfaceMapping GetRuntimeInterfaceMap(this Type type, Type interfaceType)
		{
			return type.GetInterfaceMap(interfaceType);
		}
	}
}
#endif
