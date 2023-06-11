using System.Collections.Generic;

namespace Assets.Scripts
{
    interface IDropable
    {
        public List<ItemModel> DropedItems { get; }
        public int MaxDropedItems { get; }
        
        public void DropItems();
    }
}
