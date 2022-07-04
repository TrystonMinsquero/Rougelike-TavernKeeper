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
        public string Name { get => name; private set => name = value; }
        public string Description { get => description; private set => description = value; }
        public Sprite Image { get => image; private set => image = value; }
        
        [Header("Basic Attributes")]
        [SerializeField] private new string name;
        [TextArea(1, 2)]
        [SerializeField] private string description;
        [SerializeField] private Sprite image;

        public Item(string name, string description, Sprite image)
        {
            Name = name;
            Description = description;
            Image = image;
        }

        public Item()
        {
        }
    }
}