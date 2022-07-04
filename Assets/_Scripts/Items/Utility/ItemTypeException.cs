using System;

namespace Items
{
    public class ItemTypeException : Exception
    {
        public ItemTypeException(ItemType type) : base($"ItemType {type.ToString()} is not set to be processed into an Type")
        {
        }
        public ItemTypeException(Type type) : base($"Type {type.ToString()} is not set to be processed into an ItemType")
        {
        }
        
        public ItemTypeException(string message) : base(message)
        {
            
        }

        public ItemTypeException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}