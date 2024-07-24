using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class FieldInjectorException : Exception
	{
		public FieldInjectorException(Exception e) : base(e.Message) { }
	}
}
