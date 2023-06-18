using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NaturalResource: MonoBehaviour, IDropable, IExtractable
    {
        [SerializeField] private List<ItemModel> _dropedItems;
        [SerializeField] private List<ItemType> _itemsCanExtract;
        [SerializeField] private int _maxDropedItems;
        [SerializeField] private int _strength;

        public List<ItemModel> DropedItems 
        { 
            get
            {
                return _dropedItems;
            }
            private set
            {
                _dropedItems = value;
            }
        }
        public List<ItemType> ItemsCanExtract
        {
            get
            {
                return _itemsCanExtract;
            }
            private set
            {
                _itemsCanExtract = value;
            }
        }
        public int MaxDropedItems 
        {
            get
            {
                return _maxDropedItems;
            }
            private set
            {
                _maxDropedItems = value;
            }
        }
        public int Strength
        {
            get
            {
                return _strength;
            }
            private set
            {
                _strength = value;
            }
        }

        public bool CanExtract(Item usingItem)
        {
            if (usingItem == null)
            {
                return false;
            }

            foreach (ItemType itemType in ItemsCanExtract)
            {
                if (itemType == usingItem.ItemCategory)
                {
                    return true;
                }
            }

            return false;
        }

        public void DropItems()
        {
            Destroy(gameObject);

            if (MaxDropedItems == 0)
            {
                return;
            }


            for (int i = 0; i < Random.Range(1, MaxDropedItems); i++)
            {
                string itemName = DropedItems[Random.Range(0, DropedItems.Count - 1)].name;

                Vector2 offset = new Vector2(Random.Range(0, 1.5f), Random.Range(0, 0.5f));
                Vector2 itemPosition = new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y);

                ObjectSpawner.Instance.SpawnItem(itemPosition, itemName, 1);
            }
        }

        public void Extract()
        {
            Strength--;

            if (Strength <= 0)
            {
                ClimateZoneController.Instance.OnClimateZoneObjectDestroyed.Invoke(this, new ClimateZoneObjectDestroyedEventArgs(gameObject));
                DropItems();
            }
        }
    }
}
