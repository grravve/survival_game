using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectSpawner: MonoBehaviour
    {
        public GameObject ItemPrefab;

        public static ObjectSpawner Instance { get; private set; }

        public List<ItemModel> ItemModels;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnItems()
        {
            ItemWorld.SpawnItemWorld(new Vector3(40, 70, 0), new Item(GetItemModel("OakTimber")), ItemPrefab);
            ItemWorld.SpawnItemWorld(new Vector3(42, 70, 0), new Item(GetItemModel("OakTimber")), ItemPrefab);
            ItemWorld.SpawnItemWorld(new Vector3(42, 70, 0), new Item(GetItemModel("OakTimber")), ItemPrefab);
        }

        public void SpawnItem(Vector2 position, string itemName)
        {

        }

        public void SpawnProps(List<Vector2> positions, List<GameObject> prefabs)
        {

        }

        public void SpawnAnimals(Vector2 position, GameObject prefab)
        {

        }

        public void SpawnAnimal(Vector2 position, GameObject prefab)
        {

        }

        public ItemModel GetItemModel(string name)
        {
            foreach(var item in ItemModels)
            {
                if(item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        private ItemModel GetItemModel(ItemType itemType)
        {
            foreach (var item in ItemModels)
            {
                if (item.Type == itemType)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
