using System;
using Items;
using UnityEngine;

namespace Player
{
    public class HeldItemVisual : MonoBehaviour
    {
        private SpriteRenderer _sr;
        public Item item;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        public void UpdateItem(InventorySlot slot)
        {
            if (slot.item == null || slot.item as Ingredient == null)
            {
                _sr.enabled = false;
                _sr.sprite = null;
                item = null;
            }
            else
            {
                _sr.sprite = slot.item.Image;
                _sr.enabled = true;
                item = slot.item;
            }
        }
    }
}