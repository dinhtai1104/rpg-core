using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	public sealed class UnknownContractException : Exception
	{
		public UnknownContractException(Type contract) : base(GenerateMessage(contract)) { }

		private static string GenerateMessage(Type contract)
		{
			return $"Cannot resolve contract '{contract.GetFullName()}'.";
		}
	}
}
