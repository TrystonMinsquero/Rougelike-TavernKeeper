using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Items
{
    public enum WeaponType
    {
        SwingMelee,
        Ranged
    }
    [System.Serializable][CreateAssetMenu(menuName = "Items/Weapon")]
    public class Weapon : Item
    {
        public WeaponType WeaponType {get => weaponType; private set => weaponType = value;}
        public int Damage { get => damage; private set => damage = value; }
        public float Range { get => range; private set => range = value; }
        public float Cooldown { get => cooldown; private set => cooldown = value; }
        
        public float Dps => Damage / Cooldown;

        [Header("Weapon Attributes")]
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private int damage;
        [SerializeField] private float range;
        [Min(.001f)]
        [SerializeField] private float cooldown;



    }
}