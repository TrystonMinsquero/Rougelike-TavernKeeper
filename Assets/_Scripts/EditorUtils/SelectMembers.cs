using System;
using System.Linq;
using System.Reflection;
using Animancer.Editor;
using Misc;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorUtils
{
    public class SelectMembers : PropertyAttribute
    {
        public SelectMembers()
        {
            
        }
    }

    [CustomPropertyDrawer(typeof(SelectMembers))]
    public class SelectMembersDrawer : PropertyDrawer
    {
        public string[] memberTypeNames;
        public int memberTypeSelection;
        
        public static SerializedObject obj;
        
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            // Using BeginProperty / EndProperty on the parent property means that 
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // First get the attribute since it contains the range for the slider
            var types = fieldInfo.MemberType;
            
            var memberTypes = ReflectiveEnumerator.GetEnumerableOfType(fieldInfo.FieldType).ToArray();

            memberTypeNames = memberTypes.ToList().Select(type => type.Name).ToArray();

            memberTypeSelection = EditorGUI.Popup(position, memberTypeSelection, memberTypeNames);

            position = GetNextPosition(position);

            var selectedType = memberTypes[memberTypeSelection];

            ScriptableObject selectedSO;

            // Root is a scriptable object, display fields
            if (property.GetValue() as ScriptableObject != null)
            {
                selectedSO = ScriptableObject.CreateInstance(selectedType);
                obj = new SerializedObject(selectedSO);
            }
            else
            {
                EditorGUILayout.HelpBox($"{fieldInfo.FieldType} needs to inherit from Scriptable Object", MessageType.Error, true);
                return;
            }
            
            float prevHeight = EditorGUI.GetPropertyHeight(property, label, true);
            
            // Show All fields
            EditorGUI.LabelField(position, $"{selectedType} Fields");
            position = GetNextPosition(position);
            var prop = obj.GetIterator();
            while (prop.NextVisible(true))
            {
                Debug.Log("Child is " + prop.displayName);
                Rect newRect = new Rect(position.x, position.y + prevHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUI.GetPropertyHeight(prop, label, true));
                prevHeight += newRect.height + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(newRect, prop);
            }
            obj.ApplyModifiedProperties();
            property.SetValue(obj);

            EditorGUI.EndProperty();


        }
        
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {

            SerializedObject childObj = /*new UnityEditor.SerializedObject(obj);*/ obj;
            SerializedProperty ite = childObj.GetIterator();
 
            float totalHeight = EditorGUI.GetPropertyHeight (property, label, true) + EditorGUIUtility.standardVerticalSpacing;
 
            while (ite.NextVisible(true))
            {
                totalHeight += EditorGUI.GetPropertyHeight(ite, label, true) + EditorGUIUtility.standardVerticalSpacing;
            }
 
            return totalHeight;
        }

        private Rect GetNextPosition(Rect position)
        {
            
            return new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.x,
                EditorGUIUtility.singleLineHeight + position.height);
        }
    }
}