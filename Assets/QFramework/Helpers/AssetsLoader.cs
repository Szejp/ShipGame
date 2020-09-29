using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace QFramework.Helpers
{
    public static class AssetsLoader
    {
#if UNITY_EDITOR
        public static List<T> LoadAllFromPath<T>(string loadPath, string searchPattern) where T : Object
        {
            loadPath = Application.dataPath + Path.DirectorySeparatorChar + loadPath;
            List<T> results = new List<T>();
            string[] filePaths = Directory.GetFiles(loadPath, searchPattern, SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
                string assetPath = "Assets" + filePath.Replace(Application.dataPath, "").Replace('\\', '/');
                T asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
                if (asset != null)
                    results.Add(asset);
            }

            results = results.Select(p => p).ToList();
            return results;
        }
#endif
    }
}