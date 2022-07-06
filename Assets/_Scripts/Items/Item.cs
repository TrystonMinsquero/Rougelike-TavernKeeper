using System;
using System.Collections.Generic;
using System.Linq;
using Misc;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public abstract class Item : ScriptableObject
    {
        public string Name => itemName;
        public string Description => description;
        public Sprite Image => image;
        
        [Header("Basic Attributes")]
        [SerializeField] private string itemName;
        [TextArea(1, 2)]
        [SerializeField] private string description;
        [SerializeField] private Sprite image;

        public Item(string name, string description, Sprite image)
        {
            itemName = name;
            this.description = description;
            this.image = image;
        }

        public Item()
        {
        }
    }
}