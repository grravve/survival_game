﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class Inventory
    {
        public InventorySlot CurrentSlot { get; private set; }
        public List<InventorySlot> InventorySlots { get; private set; }

        public event EventHandler OnItemListChanged;

        private int _maxSlots;

        public Inventory(int maxSlots)
        {
            _maxSlots = maxSlots;

            InventorySlots = new List<InventorySlot>(_maxSlots);

            for(int i = 0; i < _maxSlots; i++)
            {
                InventorySlots.Add(new InventorySlot());
            }

            CurrentSlot = InventorySlots[0];
        }

        public bool AddItem(Item item, int quantity)
        {
            foreach(var slot in InventorySlots)
            {
                if(slot.IsEmpty() || (slot.Item.Equals(item) && !slot.IsFull()))
                {
                    slot.AddItem(item, quantity);
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);

                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(Item item)
        {
            foreach (var slot in InventorySlots)
            {
                if (slot.Item.Equals(item))
                {
                    slot.RemoveItem();
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        public void ChangeCurrentSlot(int slotNumber)
        {
            if(slotNumber > InventorySlots.Count)
            {
                return;
            }

            CurrentSlot = InventorySlots[slotNumber - 1];
        }

        public void UseCurrentItem()
        {
            //CurrentSlot.Item.Use();
        }
    }
}
