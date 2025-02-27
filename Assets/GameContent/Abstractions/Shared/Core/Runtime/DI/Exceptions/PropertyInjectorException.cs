using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class PropertyInjectorException : Exception
	{
		public PropertyInjectorException(Exception e) : base(e.Message) { }
	}
}
