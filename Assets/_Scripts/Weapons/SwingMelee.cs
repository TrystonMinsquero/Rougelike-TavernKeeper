using System;
using System.Collections;
using Enemies;
using Items;
using Misc;
using UnityEditor;
using UnityEngine;

namespace Weapons
{
    [System.Serializable][CreateAssetMenu(menuName = "Items/SwingMelee")]
    public class SwingMelee :  Weapon
    {
        [SerializeField] public float attackRadius;
        [SerializeField] public float swingTime;

        public override IEnumerator AttackRoutine(WeaponMB weaponObj, Vector2 direction, Action onFinished = null)
        {
            weaponObj.ColliderActive(true);
            yield return new WaitForSeconds(swingTime);
            weaponObj.ColliderActive(false);
            if (onFinished != null)
                onFinished();
        }
    }
}