using System;
using JetBrains.Annotations;

namespace Assets.Abstractions.Shared.Core.DI
{
	[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
	public class InjectAttribute : Attribute { }
}
