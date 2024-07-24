using System.Linq;
using System.Reflection;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class TypeAttributeInfo
	{
		public readonly FieldInfo[] InjectableFields;
		public readonly PropertyInfo[] InjectableProperties;
		public readonly InjectedMethodInfo[] InjectableMethods;

		public TypeAttributeInfo(FieldInfo[] injectableFields, PropertyInfo[] injectableProperties, MethodInfo[] injectableMethods)
		{
			InjectableFields = injectableFields;
			InjectableProperties = injectableProperties;
			InjectableMethods = injectableMethods.Select(mi => new InjectedMethodInfo(mi)).ToArray();
		}
	}
}
