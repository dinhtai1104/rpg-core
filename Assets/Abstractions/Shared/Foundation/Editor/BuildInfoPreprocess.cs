using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation.Editors
{
    public class BuildInfoPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder => default;

        public void OnPreprocessBuild(BuildReport report)
        {
            var buildInfo = BuildInfo.Instance;
            if (buildInfo != null)
            {
                buildInfo.Version = PlayerSettings.bundleVersion;
                buildInfo.BuildId = GetBuildId();
                Debug.Log($"Build Info: {buildInfo.Version} - {buildInfo.BuildId}");
                EditorUtility.SetDirty(buildInfo);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static string GetBuildId()
        {
#if UNITY_ANDROID
            return PlayerSettings.Android.bundleVersionCode.ToString();
#elif UNITY_IOS
            return PlayerSettings.iOS.buildNumber.ToString();
#else
            return default;
#endif
        }
    }
}