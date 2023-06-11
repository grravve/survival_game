using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IExtractable
    {
        public List<ItemType> ItemsCanExtract { get; }
        public int Strength { get; }

        public bool CanExtract(Item item);
        public void Extract();
    }
}
