using System;
using EditorUtils;
using UnityEngine;

namespace Misc
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int healthMax = 10;
        [SerializeField][ReadOnly] private float currentHealth = 10;

        public Action<float> OnDamaged = delegate(float f) {  };
        public Action OnDied = delegate() {  };

        public float CurrentHealthPercent => currentHealth / healthMax;

        private void Start()
        {
            currentHealth = healthMax;
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            OnDamaged.Invoke(damage);
            if(currentHealth <= 0)
                OnDied.Invoke();
        }

        public void FillHealth() => currentHealth = healthMax;
        
        public void SetHealth(int amount) => currentHealth = amount;

    }
}