
using System;
using Items;
using Misc;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnDamaged += (amount) => Debug.Log($"{name} took {amount} damage");
            _health.OnDied += () => Debug.Log($"{name} died, bruh");
        }
        
    }
}