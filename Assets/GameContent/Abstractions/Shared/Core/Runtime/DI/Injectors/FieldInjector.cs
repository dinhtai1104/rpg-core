using System;
using System.Reflection;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal class FieldInjector
	{
		private readonly Injector _injector;

		internal FieldInjector(Injector injector)
		{
			_injector = injector;
		}

		internal void Inject(FieldInfo field, object instance)
		{
			try
			{
				var concrete = _injector.GetConcreteByContract(field.FieldType);
				if (concrete != null)
				{
					field.SetValue(instance, concrete);
				}
			}
			catch (Exception e)
			{
				throw new FieldInjectorException(e);
			}
		}
	}
}
