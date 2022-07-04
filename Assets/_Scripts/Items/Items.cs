using System;
using System.Collections.Generic;
using Items.Utility;

namespace Items
{
    [System.Serializable]
    public class Items
    {
        private static Items Instance;

        public static Item[] AllItems
        {
            get
            {
                #if UNITY_EDITOR
                Instance ??= new Items(ItemLoader.LoadAllItems());
                #endif
                return Instance._allItems;
            }
        }

        private Item[] _allItems;

        /// <summary>
        /// Will populate static lists under the current instance, and will create
        /// an instance if none exists
        /// </summary>
        /// <param name="itemLists"></param>
        private Items(params Item[][] itemLists)
        {
            if (Instance == null)
                Instance = this;
            
            // Create list of all Items
            List<Item> items = new List<Item>();

            if (Instance._allItems != null)
                items.AddRange(Instance._allItems);
            
            foreach (var itemList in itemLists)
                foreach (var item in itemList)
                    items.Add(item);
            
            Instance._allItems = items.ToArray();
        }
        public static Item[] GetItems(ItemType type)
        {
            Type T = ItemUtils.GetItemType(type);
            var items = new List<Item>();

            foreach (var item in AllItems)
                if (item.GetType() == T)
                    items.Add(item);

            return items.ToArray();

        }

        public static T[] GetItems<T>() where T : Item
        {
            var items = new List<T>();
            foreach (var item in items)
                if (item is T itemOfTypeT)
                    items.Add(itemOfTypeT);

            return items.ToArray();
        }
    }
}