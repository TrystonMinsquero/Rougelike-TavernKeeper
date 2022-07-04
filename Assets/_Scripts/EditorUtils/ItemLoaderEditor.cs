using System;
using UnityEditor;
using UnityEngine;
using Items;
using Items.Utility;
using Misc;

namespace EditorUtils
{
    [CustomEditor(typeof(ItemLoader))]
    public class ItemLoaderEditor : Editor
    {
        public bool showItems;
        public bool showAllItems;
        public bool[] showItemTypes = new bool[Enum.GetValues(typeof(ItemType)).Length];
        
        public bool showItemNames;
        public bool showAllItemsNames;
        public bool[] showItemTypesNames = new bool[Enum.GetValues(typeof(ItemType)).Length];

        private MonoScript _script;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            showItems = EditorGUILayout.Foldout(showItems, "Item Objects");
            if (showItems)
            {
                // Show All items foldout
                EditorGUI.indentLevel++;
                showAllItems = EditorGUILayout.Foldout(showAllItems, "All Items");
                if (showAllItems)
                    DisplayItemObjects(Items.Items.AllItems);
                EditorGUI.indentLevel--;
                
                // Show ItemType categories
                foreach(var itemT in typeof(ItemType).GetAll())
                {
                    
                    EditorGUI.indentLevel++;
                    showItemTypes[itemT.Value] = EditorGUILayout.Foldout(
                        showItemTypes[itemT.Value],itemT.ToString());
                    if(showItemTypes[itemT.Value])
                        DisplayItemObjects(Items.Items.GetItems(itemT as ItemType));
                    EditorGUI.indentLevel--;
                    
                }
            }
            
            showItemNames = EditorGUILayout.Foldout(showItemNames, "Item Names");
            if (showItemNames)
            {
                
                // Show All items 
                EditorGUI.indentLevel++;
                showAllItemsNames = EditorGUILayout.Foldout(showAllItemsNames, "All Items");
                if (showAllItemsNames)
                    DisplayItemNames(Items.Items.AllItems);
                EditorGUI.indentLevel--;
                
                // Show ItemType categories
                foreach(var itemT in typeof(ItemType).GetAll())
                {
                    EditorGUI.indentLevel++;
                    showItemTypesNames[itemT.Value] = EditorGUILayout.Foldout(
                        showItemTypesNames[itemT.Value],itemT.ToString());
                    if(showItemTypesNames[itemT.Value])
                        DisplayItemNames(Items.Items.GetItems(itemT as ItemType));
                    EditorGUI.indentLevel--;
                }
                
            }
        }

        private void DisplayItemObjects(Item[] items)
        {
            EditorGUI.indentLevel++;
            foreach (Item item in items)
            {
                // var obj = new SerializedObject(item);
                EditorGUILayout.ObjectField(item, typeof(Item), false);
            }
            EditorGUI.indentLevel--;
        }

        private void DisplayItemNames(Item[] items)
        {
            EditorGUI.indentLevel++;
            string names = "";
            foreach (var item in items)
            {
                names += item.Name + "\n";
            }
            EditorGUILayout.SelectableLabel(names);
            EditorGUI.indentLevel--;
        }
    }
}