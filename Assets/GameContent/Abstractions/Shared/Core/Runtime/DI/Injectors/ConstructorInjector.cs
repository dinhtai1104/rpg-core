using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal class ConstructorInjector
	{
		private readonly Injector _injector;

		internal ConstructorInjector(Injector injector)
		{
			_injector = injector;
		}

		public object Construct(Type concrete)
		{
			var info = TypeConstructionInfoCache.Get(concrete);
			var arguments = ExactArrayPool<object>.Shared.Rent(info.ConstructorParameters.Length);

			for (var i = 0; i < info.ConstructorParameters.Length; i++)
			{
				arguments[i] = _injector.GetConcreteByContract(info.ConstructorParameters[i]);
			}

			try
			{
				return info.ObjectActivator.Invoke(arguments);
			}
			catch (Exception e)
			{
				throw new ConstructorInjectorException(concrete, e);
			}
			finally
			{
				ExactArrayPool<object>.Shared.Return(arguments);
			}
		}

		public object Construct(Type concrete, object[] arguments)
		{
			var info = TypeConstructionInfoCache.Get(concrete);

			try
			{
				return info.ObjectActivator.Invoke(arguments);
			}
			catch (Exception e)
			{
				throw new ConstructorInjectorException(concrete, e);
			}
		}
	}
}
