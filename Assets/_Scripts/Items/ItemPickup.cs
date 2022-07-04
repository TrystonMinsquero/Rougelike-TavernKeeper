using UnityEngine;

namespace Items
{
    public class ItemPickup : Pickup
    {
        public Item item;

        public static ItemPickup CreateInstance(Item item, Vector3 position)
        {
            ItemPickup itemPickup =  Pickup.CreateInstance<ItemPickup>(position, item.Image);
            itemPickup.name = $"{item.name} Pickup";
            itemPickup.item = item;
            return itemPickup;
        }
    }
}