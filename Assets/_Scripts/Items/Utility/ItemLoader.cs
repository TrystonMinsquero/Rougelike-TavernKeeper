using System;
using System.Collections.Generic;
using System.IO;
using EditorUtils;
using UnityEditor;
using UnityEngine;

namespace Items.Utility
{
    [CreateAssetMenu(menuName = "Items/Utility/Loader")]
    public class ItemLoader : ScriptableObject
    {
        public static Item[] LoadAllItems()
        {
            return LoadAssetsInDirectoryRecursive<Item>(ItemUtils.RootPath);
        }

        private static T[] LoadAssetsInDirectory<T>(string pathName) where T : class
        {
            string[] fileEntries = Directory.GetFiles(pathName);
            var assetsList = new List<T>();
            foreach (var fileName in fileEntries)
            {
                var asset = AssetDatabase.LoadAssetAtPath(fileName, typeof(T));
                Debug.Log(asset);
                Debug.Log(asset as T);
                if(asset != null && asset is T assetOfTypeT)
                    assetsList.Add(assetOfTypeT);
            }
            return assetsList.ToArray();
        }
        
        private static T[] LoadAssetsInDirectoryRecursive<T>(string pathName) where T : class
        {
            var assetsList = new List<T>();
            
            // Search deep for all directories
            string[] directories = Directory.GetDirectories(pathName);

            foreach (var directory in directories)
                assetsList.AddRange(LoadAssetsInDirectoryRecursive<T>(directory));
            
            // Add files if no more directories
            string[] fileEntries = Directory.GetFiles(pathName);
            
            foreach (var fileName in fileEntries)
            {
                var asset = AssetDatabase.LoadAssetAtPath(fileName, typeof(T));
                if(asset != null && asset is T assetOfTypeT)
                    assetsList.Add(assetOfTypeT);
            }
            
            return assetsList.ToArray();
        }
    }
}
