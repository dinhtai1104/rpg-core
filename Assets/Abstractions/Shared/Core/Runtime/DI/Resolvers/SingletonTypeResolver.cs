using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class SingletonTypeResolver : IResolver
	{
		private object _instance;
		private readonly Type _concreteType;
		private readonly DisposableCollection _disposables = new();
		public Lifetime Lifetime => Lifetime.Singleton;

		public SingletonTypeResolver(Type concreteType)
		{
			Diagnosis.RegisterCallSite(this);
			_concreteType = concreteType;
		}

		public object Resolve(Injector injector)
		{
			Diagnosis.IncrementResolutions(this);
            
			if (_instance == null)
			{
				_instance = injector.Construct(_concreteType);
				_disposables.TryAdd(_instance);
				Diagnosis.RegisterInstance(this, _instance);
			}

			return _instance;
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
