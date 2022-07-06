using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class HotBarUI : MonoBehaviour
    {
        public Player player;
        public InventorySlotUI[] slots;
        public int selectedSlotIndex;

        public Inventory Inventory => player.inventory;

        private void Start()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var i1 = i;
                slots[i].Selected += () => EquipSlot(i1);
            }
            UpdateSlots();
            Inventory.SlotsChanged += UpdateSlots;
        }

        private void UpdateSlots()
        {
            UpdateSlots(Inventory);
        }

        public void UpdateSlots(Inventory inventory)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(i < inventory.InventorySlots.Length)
                    slots[i].UpdateSlot(inventory.InventorySlots[i]);
                if (slots[i].isSelected)
                    slots[i].Select(true);
            }
        }

        public void EquipSlot(int index)
        {
            try
            {
                Inventory.Equip(index);
            } catch(Exception e) {Debug.LogError(e);}

            slots[selectedSlotIndex].Select(false);
            selectedSlotIndex = index;
        }

        private void OnEnable()
        {
            Inventory.SlotsChanged += UpdateSlots;
        }
        
        private void OnDisable()
        {
            Inventory.SlotsChanged -= UpdateSlots;
        }
    }
}