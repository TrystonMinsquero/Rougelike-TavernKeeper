
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
        public Inventory inventory;

        private void Awake()
        {
            _health = GetComponent<Health>();
            inventory = new Inventory(inventory.inventorySpace);
        }

        public void Start()
        {
            _health.OnDamaged += (amount) => Debug.Log($"{name} took {amount} damage");
            _health.OnDied += () => Debug.Log($"{name} died, bruh");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("Col" + col.gameObject.name);
            if (col.gameObject.TryGetComponent<ItemPickup>(out var itemPickup))
            {
                if(inventory.Add(itemPickup.item))
                    Destroy(itemPickup.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Trigger" + col.gameObject.name);
            if (col.gameObject.TryGetComponent<ItemPickup>(out var itemPickup))
            {
                if(inventory.Add(itemPickup.item))
                    Destroy(itemPickup.gameObject);
            }
        }

    }
}