using Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorUtils
{
    [CustomEditor(typeof(ItemManager))]
    public class ItemManagerEditor : Editor
    {
        public Transform source;
        public Item item;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            source = EditorGUILayout.ObjectField(source, typeof(Transform), true) as Transform;
            item = EditorGUILayout.ObjectField(item, typeof(Item), false) as Item;

            if (GUILayout.Button("Instantiate Item"))
            {
                if (source != null)
                {
                    ItemPickup.CreateInstance(item, source.position);
                }
                else
                {
                    ItemPickup.CreateInstance(item, Vector3.zero);
                }
            }
        }
    }
}