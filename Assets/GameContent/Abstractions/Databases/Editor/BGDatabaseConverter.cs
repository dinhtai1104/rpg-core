using BansheeGz.BGDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using BansheeGz.BGDatabase.Editor;

namespace Shared.Databases.Editor
{
    public class BGDatabaseConverter : BGEditorJobStatusCallbackI
    {
        public static string AssetDatabasePath = "Assets/Database/";
        [MenuItem("Tools/BGDatabase/Convert")]
        public static void Convert()
        {
            var allMetas = BGRepo.I.FindMetas();
            var allAssemply = TypeCache.GetTypesDerivedFrom<DataTableAsset>();

            // create assembly
            foreach (var type in allAssemply)
            {
                if (type.IsAbstract) continue;
                CreateOrGetDataTable(type);
            }
        }

        private static void CreateOrGetDataTable(Type type)
        {
            var path = AssetDatabasePath + type.Name + ".asset";

            var data = AssetDatabaseUtils.GetAssetOfType<DataTableAsset>(type.Name + ".asset");
            if (data == null)
            {
                data = ScriptableObject.CreateInstance(type) as DataTableAsset;
                AssetDatabase.CreateAsset(data, path);
            }
            data.Initialize();
            AssetDatabase.SaveAssets();
        }

        public void OnFinished(BGEditorJobStatusContext context)
        {
            switch (context.Operation)
            {
                case BGEditorJobStatusContext.OperationTypeEnum.Import:
                    Convert();
                    break;
                case BGEditorJobStatusContext.OperationTypeEnum.Export:
                    break;
            }
        }


        public class AssetDatabaseUtils
    {
        public static string GetSelectionObjectPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }

        public static T GetAssetOfType<T>(string name, System.Type mainType = null) where T : class
        {
            if (mainType == null)
            {
                mainType = typeof(T);
            }

            var guids = AssetDatabase.FindAssets(name + " t:" + mainType.Name);
            if (guids.Length == 0)
                return null;
            string guid = guids[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            foreach (var o in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                var res = o as T;
                if (res != null)
                {
                    return res;
                }
            }

            return default(T);
        }

        public static string GetAssetPathOfType<T>(string name, System.Type mainType = null) where T : class
        {
            if (mainType == null)
            {
                mainType = typeof(T);
            }

            var guids = AssetDatabase.FindAssets(name + " t:" + mainType.Name);
            if (guids.Length == 0)
                return null;
            string guid = guids[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return path;
        }

        public static T GetAssetOfType<T>(bool unique = false) where T : class
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).FullName);
            if (guids.Length == 0)
                return null;
            if (guids.Length > 1 && unique)
            {
                var pathes = "";
                foreach (var g in guids)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(g);
                    pathes += assetPath + "\n";
                }

                throw new System.ArgumentException("Has multiple objects with this type: \n" + pathes);
            }

            var guid = guids[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
        }

        public static T[] GetAssetsOfType<T>() where T : class
        {
            if (typeof(UnityEngine.Component).IsAssignableFrom(typeof(T)))
            {
                var guidsGO = AssetDatabase.FindAssets("t:Prefab");
                var l = new List<T>();
                foreach (var g in guidsGO)
                {
                    var path = AssetDatabase.GUIDToAssetPath(g);
                    var t = (AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject).GetComponent<T>();
                    if (t != null)
                    {
                        l.Add(t);
                    }
                }

                return l.ToArray();
            }

            var guids = AssetDatabase.FindAssets("t:" + typeof(T).FullName);
            if (guids.Length == 0)
                return null;

            var i = 0;
            var res = new T[guids.Length];
            foreach (var g in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                var t = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
                res[i] = t;
                i++;
            }

            return res;
        }
    }
    }
}