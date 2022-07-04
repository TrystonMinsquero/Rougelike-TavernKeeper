using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using Object = UnityEngine.Object;


public class BrainConstructor : EditorWindow
{
    [MenuItem("Window/Brain Constructor")]
    private static void Init()
    {
        var window = GetWindow<BrainConstructor>();
        window.Show();
    }

    public Object layer;

    private void OnGUI()
    {
        var obj = EditorGUILayout.ObjectField("Layer", layer, typeof(AnimatorController), false);
    }
}
