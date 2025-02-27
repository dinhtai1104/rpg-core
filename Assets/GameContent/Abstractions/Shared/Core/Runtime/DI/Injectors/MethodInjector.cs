using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal class MethodInjector
	{
		private readonly Injector _injector;

		internal MethodInjector(Injector injector)
		{
			_injector = injector;
		}

		internal void Inject(InjectedMethodInfo method, object instance)
		{
			var arguments = ExactArrayPool<object>.Shared.Rent(method.Parameters.Length);

			try
			{
				for (var i = 0; i < method.Parameters.Length; i++)
				{
					var concrete = _injector.GetConcreteByContract(method.Parameters[i].ParameterType);
					if (concrete != null)
					{
						arguments[i] = concrete;
					}
				}

				method.MethodInfo.Invoke(instance, arguments);
			}
			catch (Exception e)
			{
				throw new MethodInjectorException(instance, method.MethodInfo, e);
			}
			finally
			{
				ExactArrayPool<object>.Shared.Return(arguments);
			}
		}
	}
}
