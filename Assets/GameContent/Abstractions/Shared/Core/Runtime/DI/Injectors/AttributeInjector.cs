namespace Assets.Abstractions.Shared.Core.DI
{
	internal class AttributeInjector
	{
		private readonly PropertyInjector _propertyInjector;
		private readonly FieldInjector _fieldInjector;
		private readonly MethodInjector _methodInjector;

		internal AttributeInjector(Injector injector)
		{
			_propertyInjector = new PropertyInjector(injector);
			_fieldInjector = new FieldInjector(injector);
			_methodInjector = new MethodInjector(injector);
		}

		public void Inject(object obj)
		{
			var info = TypeInfoCache.Get(obj.GetType());

			for (var i = 0; i < info.InjectableFields.Length; i++)
			{
				_fieldInjector.Inject(info.InjectableFields[i], obj);
			}

			for (var i = 0; i < info.InjectableProperties.Length; i++)
			{
				_propertyInjector.Inject(info.InjectableProperties[i], obj);
			}

			for (var i = 0; i < info.InjectableMethods.Length; i++)
			{
				_methodInjector.Inject(info.InjectableMethods[i], obj);
			}
		}
	}
}
