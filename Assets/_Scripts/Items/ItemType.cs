namespace Items
{
	[System.Serializable]
	public class ItemType : Enumeration
	{
		// Members
		public static readonly ItemType Ingredient = new ItemType(0,"Ingredient");
		public static readonly ItemType Weapon = new ItemType(1,"Weapon");

		// Constructors
		private ItemType() { }
		private ItemType(int value, string displayName) : base(value, displayName) { }
	}
}