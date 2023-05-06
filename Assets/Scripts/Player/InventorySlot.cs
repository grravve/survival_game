using System;

namespace Assets.Scripts
{
    public class InventorySlot
    {
        public Item Item { get; private set; }
        
        private int _quantity;

        public InventorySlot()
        {
            Item = null;
            _quantity = 0;
        }

        public void AddItem(Item newItem)
        {
            if(Item == null)
            {
                Item = newItem;
            }

            if(Item.Equals(newItem))
            {
                _quantity++;
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
            if(_quantity >= Item.MaxStack)
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
