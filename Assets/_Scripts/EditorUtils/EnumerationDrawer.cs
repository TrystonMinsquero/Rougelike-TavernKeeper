using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Animancer.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorUtils
{
    [CustomPropertyDrawer(typeof(Enumeration), true)]
    public class EnumerationDrawer : PropertyDrawer
    {
        public int memberTypeSelection;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // List<SerializedProperty> props = new List<SerializedProperty>();
            var enumeration = property.GetValue<Enumeration>();
            
            var memberTypeNames = enumeration.GetAll().Select(enumType => enumType.DisplayName).ToArray();
            
            memberTypeSelection = EditorGUI.Popup(position, memberTypeSelection, memberTypeNames);

            var selectedEnumMember = enumeration.GetAll().ToArray()[memberTypeSelection];

            property.SetValue(selectedEnumMember);

            EditorGUI.EndProperty();
        }
    }
}