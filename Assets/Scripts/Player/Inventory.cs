using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class Inventory
    {
        public InventorySlot CurrentSlot { get; private set; }

        private List<InventorySlot> _inventorySlots;
        private int _maxSlots;

        public Inventory(int maxSlots)
        {
            _maxSlots = maxSlots;

            _inventorySlots = new List<InventorySlot>(_maxSlots);

            for(int i = 0; i < _maxSlots; i++)
            {
                _inventorySlots.Add(new InventorySlot());
            }
        }

        public bool AddItem(Item item)
        {
            // cycle that to find slot with needed item
            foreach(var slot in _inventorySlots)
            {
                if(slot.IsEmpty())
                {
                    slot.AddItem(item);
                    return true;
                }

                if(slot.Item.Equals(item) && !slot.IsFull())
                {
                    slot.AddItem(item);
                    return true;
                }
            }

            return false;
        }

        public void UseCurrentItem()
        {
            //CurrentSlot.Item.Use();
        }
    }
}
