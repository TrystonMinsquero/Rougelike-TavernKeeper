using System;
using System.Collections;
using Enemies;
using Items;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponMB : MonoBehaviour
    {
        public Weapon Weapon;
        public bool onCooldown => lastUsedTime > Time.time + Weapon.Cooldown;

        protected float lastUsedTime = 0;
        private void Start()
        {
            if (!Weapon)
                Debug.LogWarning("Weapon not assigned"); 
        }

        public virtual void Use(Vector2 direction, Action afterAttack = null)
        {
            Debug.Log(gameObject);
            if (Weapon == null || onCooldown)
                return;
            lastUsedTime = Time.time;
            Debug.Log("Attack: " + direction);
            StartCoroutine(AttackRoutine(direction, afterAttack));

        }

        protected abstract IEnumerator AttackRoutine(Vector2 direction, Action afterAttack = null);

        protected virtual void OnDrawGizmosSelected()
        {
            if (!Weapon)
                return;
            var pointOfAttack = (Vector2)transform.position + Vector2.right * Weapon.Range;
            Gizmos.DrawWireSphere(pointOfAttack, .5f);
        }
    }
}