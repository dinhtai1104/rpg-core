using UnityEngine;

namespace Assets.Abstractions.Shared.Core
{
    public abstract class ServiceSettings : ScriptableObject
    {
        private const string SettingsPath = "Assets/Plugins/Sparkle/Settings";

        protected virtual bool IsEditorOnly => false;

        public abstract string PackageName { get; }

        protected virtual string SettingsFolderPath
        {
            get
            {
                var folderPath = $"{SettingsPath}/{PackageName}";
                if (IsEditorOnly)
                {
                    folderPath += "/Editor";
                }

                folderPath += "/Resources";
                return folderPath;
            }
        }

        protected virtual string SettingsFileName => GetType().Name;

        public virtual string SettingsFilePath => $"{SettingsFolderPath}/{SettingsFileName}.asset";
    }
}