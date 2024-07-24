using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class TransientTypeResolver : IResolver
	{
		private readonly Type _concreteType;
		private readonly DisposableCollection _disposables = new();
		public Lifetime Lifetime => Lifetime.Transient;

		public TransientTypeResolver(Type concreteType)
		{
			Diagnosis.RegisterCallSite(this);
			_concreteType = concreteType;
		}

		public object Resolve(Injector injector)
		{
			Diagnosis.IncrementResolutions(this);
			var instance = injector.Construct(_concreteType);
			_disposables.TryAdd(instance);
			Diagnosis.RegisterInstance(this, instance);
			return instance;
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
