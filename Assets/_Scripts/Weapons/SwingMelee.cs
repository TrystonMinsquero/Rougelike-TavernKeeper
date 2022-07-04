using System;
using System.Collections;
using Enemies;
using Misc;
using UnityEngine;

namespace Weapons
{
    public class SwingMelee : WeaponMB
    {
        [SerializeField] protected float attackRadius;
        
        protected override IEnumerator AttackRoutine(Vector2 direction, Action afterAttack = null)
        {
            var pointOfAttack = (Vector2)transform.position + direction * Weapon.Range;
            var collisions = Physics2D.OverlapCircleAll(pointOfAttack, attackRadius);
            foreach (var collision in collisions)
            {
                if (collision.CompareTag(Enemy.Tag) && collision.TryGetComponent<Health>(out var health))
                {
                    health.Damage(Weapon.Damage);
                }
            }
            yield return null;
        }

        protected override void OnDrawGizmosSelected()
        {
            if (!Weapon)
                return;
            var pointOfAttack = (Vector2)transform.position + Vector2.right * Weapon.Range;
            Gizmos.DrawWireSphere(pointOfAttack, attackRadius);
        }
    }
}