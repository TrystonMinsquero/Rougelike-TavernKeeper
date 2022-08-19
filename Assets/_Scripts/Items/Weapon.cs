using System;
using System.Collections;
using EditorUtils;
using UnityEngine;
using Weapons;

namespace Items
{

    
    [System.Serializable]
    public abstract class Weapon : Item
    {
        public int Damage { get => damage; private set => damage = value; }
        public float Range { get => range; private set => range = value; }
        public float CooldownTime { get => cooldownTime; private set => cooldownTime = value; }
        public AnimatorOverrideController AOC => animatorOverrideController;

        public float Dps => Damage / CooldownTime;

        [Header("Weapon Attributes")]
        [SerializeField] private int damage;
        [SerializeField] private float range = 1;
        [Min(.001f)]
        [SerializeField] private float cooldownTime = 1;
        [SerializeField] private AnimatorOverrideController animatorOverrideController;

        public abstract IEnumerator AttackRoutine(WeaponMB weaponObj, Vector2 direction, Action onFinished = null);

    }
}