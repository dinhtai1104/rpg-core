namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class SingletonValueResolver : IResolver
	{
		private readonly object _value;
		private readonly DisposableCollection _disposables = new();
		public Lifetime Lifetime => Lifetime.Singleton;

		public SingletonValueResolver(object value)
		{
			Diagnosis.RegisterCallSite(this);
			Diagnosis.RegisterInstance(this, value);
			_value = value;
			_disposables.TryAdd(value);
		}

		public object Resolve(Injector injector)
		{
			Diagnosis.IncrementResolutions(this);
			return _value;
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
