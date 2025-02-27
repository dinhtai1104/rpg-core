using System;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal sealed class ContractDefinitionException : Exception
	{
		public ContractDefinitionException(Type concrete, Type contract) : base(GenerateMessage(concrete, contract)) { }

		private static string GenerateMessage(Type concrete, Type contract)
		{
			return $"{concrete.GetFullName()} does not implement {contract.GetFullName()} contract";
		}
	}
}
