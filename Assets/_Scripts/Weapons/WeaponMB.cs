using System;
using System.Collections;
using Animancer;
using EditorUtils;
using Enemies;
using Items;
using Misc;
using UnityEditor.Animations;
using UnityEngine;

namespace Weapons
{
    public enum WeaponState
    {
        Held,
        Attacking,
        Charging,
        Charged,
    }
    public class WeaponMB : MonoBehaviour
    {
        [Expandable] public Weapon Weapon;
        
        [SerializeField][ReadOnly] public WeaponState WeaponState;

        private Animator _anim;
        private BoxCollider2D _bc;
        private SpriteRenderer _sr;

        public bool OnCooldown => _lastAttackTime + Weapon.CooldownTime > Time.time;

        private float _lastAttackTime = float.NegativeInfinity;

        
        
        private void Start()
        {
            if (!Weapon)
                Debug.LogWarning("Weapon not assigned");
            _anim = GetComponent<Animator>();
            _bc = GetComponent<BoxCollider2D>();
            _sr = GetComponent<SpriteRenderer>();
            SwapWeapons(Weapon);
        }

        public void ColliderActive(bool active) => _bc.enabled = active;

        public virtual void Use(Vector2 direction, Action afterAttack = null)
        {
            if (Weapon == null)
                return;

            // internally checks if will actually charge
            ChargeUp(Time.deltaTime);
            
            if (CanAttack())
            {
                Debug.Log("Attack: " + direction);
                Attack(direction);
            }
        }

        /// <summary>
        /// Swaps weapons
        /// </summary>
        /// <param name="newWeapon"></param>
        /// <returns>old weapon</returns>
        public Weapon SwapWeapons(Weapon newWeapon)
        {
            var oldWeapon = Weapon;
            Weapon = newWeapon;
            if (newWeapon)
            {
                _sr.sprite = Weapon.Image;
                _anim.runtimeAnimatorController = Weapon.AOC;
                

                // updated box collider size
                _bc.offset = Vector2.zero;
                Vector3 spriteSize = _sr.sprite.bounds.size;
                Vector3 lossyScale = transform.lossyScale;
                _bc.size = new Vector3(spriteSize.x /lossyScale.x,
                    spriteSize.y /lossyScale.y,
                    spriteSize.z / lossyScale.z);
            }
            return oldWeapon;
        }
        
        public virtual bool CanAttack()
        {
            if (OnCooldown)
                return false;
            if (WeaponState.Attacking == WeaponState)
                return false;
            if (Weapon is ICanCharge chargeable)
            {
                if (!chargeable.GetCharge().IsCharged)
                    return false;
                return true;
            }
            return true;
        }

        public void Reset()
        {
            _lastAttackTime = float.NegativeInfinity;
            WeaponState = WeaponState.Held;
        }

        public virtual void ChargeUp(float amount)
        {
            if (Weapon is ICanCharge chargeable)
            {
                chargeable.GetCharge().AddChargeProgress(Time.deltaTime);
                var progress = chargeable.GetCharge().ChargeProgress;
                if (progress <= 0)
                    WeaponState = WeaponState.Held;
                else if (progress < 1)
                    WeaponState = WeaponState.Charging;
                else
                    WeaponState = WeaponState.Charged;
            }
        }

        public virtual void Attack(Vector2 direction, Action onFinished = null)
        {
            _lastAttackTime = Time.time;
            WeaponState = WeaponState.Attacking;
            
            StartCoroutine(Weapon.AttackRoutine(this, direction, () =>
            {
                if(onFinished != null)
                    onFinished();
                if(this is ICanCharge chargeable)
                    chargeable.GetCharge().Reset();
                WeaponState = WeaponState.Held;
            }));
        }

        private void Update()
        {
            SetAnimation();
        }

        protected void SetAnimation()
        {
            _anim.Play(WeaponState.ToString());
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (!Weapon)
                return;
            var pointOfAttack = (Vector2)transform.position + Vector2.right * Weapon.Range;
            if(Weapon is SwingMelee swingMelee)
                Gizmos.DrawWireSphere(pointOfAttack, swingMelee.attackRadius);
            else
                Gizmos.DrawWireSphere(pointOfAttack, .5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag(Enemy.Tag) && col.TryGetComponent<Health>(out var health))
                health.Damage(Weapon.Damage);
        }
    }
}