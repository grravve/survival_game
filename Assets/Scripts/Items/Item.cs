using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Item
    {
        public ItemType ItemCategory { get; private set; }
        public int MaxStack { get; private set; }
        public string Name { get; private set; }
        public Sprite ItemSprite { get; private set; }

        public Item(ItemModel model)
        {
            ItemCategory = model.Type;
            MaxStack = model.MaxInSlot;
            Name = model.Name;
            ItemSprite = model.ItemSprite;
        }

        public Item(Item item)
        {
            ItemCategory = item.ItemCategory;
            MaxStack = item.MaxStack;
            Name = item.Name;
            ItemSprite = item.ItemSprite;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Item item = (Item)obj;

            if(item.Name != this.Name)
            {
                return false;
            }

            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
