﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Items;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Inventory
    {
        [SerializeField] public int equippedSlotIndex = 0; 
        // [SerializeField] public int inventorySpace;
        [SerializeField] private int stackSize;
        [SerializeField] private InventorySlot[] inventorySlots;

        public InventorySlot[] InventorySlots => inventorySlots;

        public event Action SlotsChanged = delegate {  };
        public event Action<InventorySlot> Equipped = delegate(InventorySlot slot) {  };

        // public Inventory(int inventorySpace)
        // {
        //     this.inventorySpace = inventorySpace;
        //     inventorySlots = new InventorySlot[inventorySpace];
        // }

        public int Equip(int index)
        {
            if (index < 0 || index > inventorySlots.Length)
                throw new Exception($"Index {index} is out of range of the inventory");
            var oldSlotIndex = equippedSlotIndex;
            equippedSlotIndex = index;
            Equipped.Invoke(inventorySlots[index]);
            return oldSlotIndex;
        } 

        public bool Add(Item item)
        {
            var added = AddInternal(item);
            if(added)
                SlotsChanged.Invoke();
            return added;
        }

        private bool AddInternal(Item item)
        {
            int firstEmpty = -1;
            for(int i = 0; i < inventorySlots.Length; i++ )
            {
                
                var slot = inventorySlots[i];
                if (slot.IsNothing() && firstEmpty < 0)
                    firstEmpty = i;
                if (!slot.IsNothing() && slot.item.Name == item.Name && slot.amount < stackSize)
                {
                    inventorySlots[i].amount += 1;
                    return true;
                }
            }

            if (firstEmpty < 0)
                return false;

            inventorySlots[firstEmpty] = new InventorySlot(item);
            return true;
        }
    }

    [System.Serializable]
    public struct InventorySlot
    {
        public Item item;
        public int amount;

        public InventorySlot(Item item, int amount = 1)
        {
            this.item = item;
            this.amount = amount;
        }

        public static InventorySlot Nothing => new InventorySlot(null, 0);
        public bool IsNothing() => item == null;
    }
}