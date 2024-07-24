using System;
using System.Reflection;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal interface IActivatorFactory
	{
		ObjectActivator GenerateActivator(Type type, ConstructorInfo constructor, Type[] parameters);
		ObjectActivator GenerateDefaultActivator(Type type);
	}
}
