using System.Runtime.CompilerServices;

namespace Assets.Abstractions.Shared.Core.DI
{
	public static class ResolverExtensions
	{
		private static readonly ConditionalWeakTable<IResolver, ResolverDebugProperties> _registry = new();

		public static ResolverDebugProperties GetDebugProperties(this IResolver resolver)
		{
			return _registry.GetOrCreateValue(resolver);
		}
	}
}
