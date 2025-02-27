using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Abstractions.Shared.Core
{
	internal sealed class ArchitectureInstaller
	{
		private const RuntimeInitializeLoadType InitializeLoadType = RuntimeInitializeLoadType.BeforeSceneLoad;

		[RuntimeInitializeOnLoadMethod(InitializeLoadType)]
		private static void OnLoad()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var architectureBaseType = typeof(Architecture<>);
			var derivedType = assemblies
				.SelectMany(assembly => assembly.GetTypes())
				.FirstOrDefault(t =>
					t.BaseType != null &&
					t.BaseType.IsGenericType &&
					t.BaseType.GetGenericTypeDefinition() == architectureBaseType);

			if (derivedType == null)
			{
				return;
			}

			var specificType = architectureBaseType.MakeGenericType(derivedType);
			var instanceProperty = specificType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
			instanceProperty?.GetValue(null);
		}
	}
}
