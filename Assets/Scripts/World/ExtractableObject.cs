using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExtractableObject : MonoBehaviour, IDropable
    {
        public List<ItemModel> DropedItems 
        { 
            get 
            { 
                return _dropedItems; 
            } 
            set 
            { 
                _dropedItems = value; 
            } 
        }

        public int MaxDropedItems 
        { 
            get
            {
                return _maxDropedItems;
            }
            set
            {
                _maxDropedItems = value;
            }
        }

        [SerializeField] private int _maxDropedItems;
        [SerializeField] private List<ItemModel> _dropedItems;

        [SerializeField] private int _strength;

        protected void Start()
        {
            DropedItems = _dropedItems;
            MaxDropedItems = _maxDropedItems;
        }

        public void DecreaseStrength(int value)
        {
            _strength -= value;

            if(_strength <= 0)
            {
                DropItems();
            }
        }

        public void DropItems()
        {
            Destroy(gameObject);

            if(_maxDropedItems == 0)
            { 
                return;
            }

            for(int i = 0; i < Random.Range(1, _maxDropedItems); i++)
            {
                string itemName = _dropedItems[Random.Range(0, _dropedItems.Count - 1)].name;

                Vector2 offset = new Vector2(Random.Range(0, 1.5f), Random.Range(0, 0.5f));
                Vector2 itemPosition = new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y);

                ObjectSpawner.Instance.SpawnItem(itemPosition, itemName);
            }
        }
    }
}
