using System.Collections.Generic;

namespace Assets.Scripts
{
    interface IDropable
    {
        public List<ItemModel> DropedItems { get; set; }
        public int MaxDropedItems { get; set; }
        
        public void DropItems();
    }
}
