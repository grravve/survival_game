﻿using System;

namespace Assets.Scripts
{
    public class InventorySlot
    {
        public Item Item { get; private set; }
        
        public int Quantity { get; private set; }

        public InventorySlot()
        {
            Item = null;
            Quantity = 0;
        }

        public void AddItem(Item newItem)
        {
            if(Item == null)
            {
                Item = newItem;
                Quantity++;

                return;
            }

            if(Item.Equals(newItem))
            {
                Quantity++;
            }
        }

        public bool IsEmpty()
        {
            if(Item == null)
            {
                return true;
            }

            return false;
        }

        public bool IsFull()
        {
            if(Quantity >= Item.MaxStack)
            {
                return true;
            }

            return false;
        }

        public void RemoveItem()
        {

        }
    }
}
