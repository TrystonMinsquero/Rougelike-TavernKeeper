using System;
using System.IO;

namespace Items.Utility
{
    public class ItemUtils
    {
        #region Pathing Constants
        public static string RootPath => Path.Combine("Assets", "ScriptableObjects", "Items");
        public static string WeaponsPath => Path.Combine(RootPath, "Weapons");
        public static string IngredientsPath => Path.Combine(RootPath, "Ingredients");

        #endregion

        #region ItemType Conversions
        public static ItemType GetItemType(Type type)
        {
            foreach(ItemType itemType in Enum.GetValues(typeof(ItemType)))
                if (type.Name == itemType.ToString())
                    return itemType;

            throw new ItemTypeException(type);
        }

        public static Type GetItemType(ItemType type)
        {
            try
            {
                return Type.GetType("Items." + type.ToString());
            }
            catch (Exception e)
            {
                throw new ItemTypeException(type);
            }
        }
        #endregion
    }
}