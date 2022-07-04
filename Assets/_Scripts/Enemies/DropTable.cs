using System.Collections.Generic;
using System.Linq;
using EditorUtils;
using Items;
using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public class DropTable
    {
        [Tooltip("If true, change to get all rows, otherwise only selects one")]
        public bool Inclusive = true;
        public DropRow[] DropRows;

        public Drop[] GetDrop()
        {
            if (Inclusive)
            {
                List<Drop> drops = new List<Drop>();
                foreach (DropRow dropRow in DropRows)
                {
                    var drop = dropRow.GetDrop();
                    if(!Drop.IsNothing(drop))
                        drops.Add(drop);
                }
                return drops.ToArray();
            }
            else
            {
                var row = DropRows[Random.Range(0, DropRows.Length)];
                return new Drop[] {row.GetDrop()};
            }
        }

    }
    
    [System.Serializable]
    public struct DropRow
    {
        public Drop[] drops;
        public float CompleteDropChance => drops.Sum(drop => drop.rate);

        public Drop GetDrop()
        {
            var value = Random.Range(0f, 1f);
            float sum = 0;
            foreach (Drop dropRate in drops)
            {
                sum += dropRate.rate;
                if (value <= sum)
                    return dropRate;
            }

            return Drop.Nothing;
        }
    }

    [System.Serializable]
    public struct Drop
    {
        public Item item;
        [Min(0)]
        public int quantity;
        [Range(0, 1)]
        public float rate;

        public Drop(Item item, int quantity = 0, float rate = 0)
        {
            this.item = item;
            this.quantity = quantity;
            this.rate = rate;
        }

        public static Drop Nothing = new Drop(null);

        public static bool IsNothing(Drop drop) => drop.item == null;
    }
}