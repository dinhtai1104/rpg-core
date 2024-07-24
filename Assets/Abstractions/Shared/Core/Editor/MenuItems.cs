using Assets.Abstractions.Shared.Core.DI.Editors;
using UnityEditor;

namespace Assets.Abstractions.Shared.Core.DI.Editors
{
	internal static class MenuItems
	{
		[MenuItem("Sparkle/Dependency Container Debugger", priority = 4999)]
		public static void OpenReflexDebuggingWindow()
		{
			EditorWindow.GetWindow<DiDebuggerWindow>(false, "Di Debugger", true);
		}
	}
}
