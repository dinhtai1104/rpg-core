using System;
using System.Reflection;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal class PropertyInjector
	{
		private readonly Injector _injector;

		internal PropertyInjector(Injector injector)
		{
			_injector = injector;
		}

		internal void Inject(PropertyInfo property, object instance)
		{
			try
			{
				var concrete = _injector.GetConcreteByContract(property.PropertyType);
				if (concrete != null)
				{
					property.SetValue(instance, concrete);
				}
			}
			catch (Exception e)
			{
				throw new PropertyInjectorException(e);
			}
		}
	}
}
