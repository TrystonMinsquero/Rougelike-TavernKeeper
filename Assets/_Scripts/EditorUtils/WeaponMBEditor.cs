using Items;
using UnityEditor;
using Weapons;

namespace EditorUtils
{
    [CustomEditor(typeof(WeaponMB), true)]
    public class WeaponMBEditor : Editor
    {
        public Weapon weapon;
    
        public override void OnInspectorGUI()
        {
            WeaponMB weaponMB = (WeaponMB) target;
            
            base.OnInspectorGUI();
    
            if (weaponMB.Weapon == null)
                return;
    
            var obj = new SerializedObject(weaponMB.Weapon);
            
            SerializedProperty damage = obj.FindProperty("damage");
            SerializedProperty range = obj.FindProperty("range");
            SerializedProperty cooldown = obj.FindProperty("cooldown");
    
            EditorGUILayout.PropertyField(range);
            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(cooldown);
            EditorGUILayout.FloatField("DPS", weaponMB.Weapon.Dps);
    
            obj.ApplyModifiedProperties();
        }
    }
}