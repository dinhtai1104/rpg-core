using System;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Abstractions.Shared.Core
{
    public abstract class ServiceSettingsSingleton<T> : ServiceSettings where T : ServiceSettings
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load(typeof(T).Name) as T;

                    if (_instance == null)
                    {
                        _instance = CreateInstance<T>();
                        SaveToAssetDatabase(_instance);
                    }
                }

                return _instance;
            }
        }

        public static void Save()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(Instance);
#endif
        }

        private static void SaveToAssetDatabase(T asset)
        {
#if UNITY_EDITOR
            var path = Path.Combine(asset.SettingsFilePath);
            var directory = Path.GetDirectoryName(path);

            if (directory == null)
            {
                throw new InvalidOperationException($"Failed to get directory from package settings path: {path}");
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(asset, path);
#endif
        }
    }
}