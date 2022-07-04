#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Animancer;
using EditorUtils;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

// Original: https://stackoverflow.com/a/70953713
// Edited by Tryston Minsquero on July 2022

public class EnumGeneratorWindow : EditorWindow
{

    private MonoScript sourceScript;
    private string targetPath;
    private string[] pathOptions;
    private int pathChoice;

    [MenuItem("Window/Enum SubType Generator")]
    private static void Init()
    {
        var window = GetWindow<EnumGeneratorWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enum Generator", EditorStyles.boldLabel);

        sourceScript = EditorGUILayout.ObjectField("Source", sourceScript, typeof(MonoScript), false) as MonoScript;
        // EnumEx = EditorGUILayout.ObjectField("Enumeration", EnumEx, typeof(Enumeration), false) as EnumerationEx;
        // EditorGUILayout.PropertyField(EnumEx);
        
        if (!sourceScript)
        {
            EditorGUILayout.HelpBox("Reference the script where to fetch the fields from", MessageType.None, true);
            return;
        }

        var sourceType = sourceScript.GetClass();

        // foreach(var thing in Assembly.GetAssembly(sourceScript.GetType()).GetTypes().Where(x => x.IsEnum))
        //     Debug.Log(thing);

        if (sourceType == null)
        {
            EditorGUILayout.HelpBox("Could not get Type from source file!", MessageType.Error, true);
            return;
        }
        
        
        targetPath = GetDefaultEnumSubTypePath(sourceScript);
        
        // See if the file already exists
        pathOptions = GetPotentialAssetPaths(sourceType.Name + "Type");
        
        // Didn't find any files
        if (pathOptions.IsNullOrEmpty())
        {
            targetPath = EditorGUILayout.TextField(targetPath);
        }
        else // Found files
        {
            // Added none type if selected paths are bad
            pathOptions = pathOptions.Append("Custom").ToArray();

            pathChoice = EditorGUILayout.Popup("Use preexisting file path", pathChoice, pathOptions);

            // If chose None 
            if (pathChoice == pathOptions.Length - 1)
            {
                targetPath = EditorGUILayout.TextField(targetPath);
            }
            else
            {
                targetPath = pathOptions[pathChoice];
            }
        }


        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
        
        // If a path was chosen, display Previous
        if (!pathOptions.IsNullOrEmpty() && pathChoice >= 0 && pathChoice < pathOptions.Length -1)
        {
            var currentFileContent = AssetDatabase.LoadAssetAtPath<MonoScript>(targetPath);
            EditorGUILayout.LabelField("Before", EditorStyles.boldLabel);
            EditorGUILayout.TextArea(currentFileContent.text);
            EditorGUILayout.LabelField("After", EditorStyles.boldLabel);
        }

        var fileContent = EnumWriter.CreateEnumSubTypeScript(sourceType);

        fileContent = EditorGUILayout.TextArea(fileContent.ToString());

        var color = GUI.color;
        // GUI.color = Color.red;
        GUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("! DANGER ZONE !", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            if (GUILayout.Button("GENERATE ENUM"))
            {
                EnumWriter.GenerateCode(targetPath, fileContent);
            }
        }
        GUILayout.EndVertical();
        GUI.color = color;
    }

    // Get all paths that are MonoScripts from a filter
    private static string[] GetPotentialAssetPaths(string filter)
    {
        List<string> paths = new List<string>();
        foreach (var guid in AssetDatabase.FindAssets(filter))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            if(asset)
                paths.Add(path);
        }
        return paths.ToArray();
    }

    private static string GetDefaultEnumSubTypePath(MonoScript script)
    {
        var sourcePath = AssetDatabase.GetAssetPath(script);
        return sourcePath.Remove(sourcePath.IndexOf(".cs")) + "Type.cs";
    }
}
#endif