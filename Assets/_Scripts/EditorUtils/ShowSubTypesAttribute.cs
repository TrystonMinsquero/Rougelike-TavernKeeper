using System;
using System.Linq;
using Animancer.Editor;
using Items;
using Misc;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

namespace EditorUtils
{
    public class ShowSubTypesAttribute : PropertyAttribute
    {
        
    }

    [CustomPropertyDrawer(typeof(ShowSubTypesAttribute))]
    public class ShowSubTypesDrawer : PropertyDrawer
    {
        public int subTypeIndex;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // if (typeof(IBaseType).IsAssignableFrom(fieldInfo.FieldType))
            // {
            //     EditorGUI.HelpBox(position, $"{fieldInfo.FieldType} must implement the IBaseType Interface to show subtypes", MessageType.Error);
            //     return;
            // }

            var subTypes = ReflectiveEnumerator.GetEnumerableOfType(fieldInfo.FieldType).ToArray();

            var subTypeNames = subTypes.Select(type => type.Name).ToArray();
            
            subTypeIndex = EditorGUI.Popup(position, subTypeIndex, subTypeNames);
            position = new Rect(position.x, position.y  - 10 + position.height + EditorGUIUtility.standardVerticalSpacing,
                position.width, EditorGUI.GetPropertyHeight(property, label, true));

            var selectedSubType = subTypes[subTypeIndex];

            if (selectedSubType.IsSubclassOf(typeof(ScriptableObject)))
            {
                var scriptableObj = ScriptableObject.CreateInstance(selectedSubType);
                var so = new SerializedObject(scriptableObj);
                var obj = EditorGUI.ObjectField(position, scriptableObj, typeof(ScriptableObject),
                    false);
                property.SetValue(obj);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType.IsSubclassOf(typeof(ScriptableObject)))
                return EditorGUI.GetPropertyHeight (property, label, true) + EditorGUIUtility.singleLineHeight;
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}