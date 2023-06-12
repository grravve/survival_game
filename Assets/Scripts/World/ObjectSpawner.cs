using System.Collections.Generic;
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
            ItemWorld.SpawnItemWorld(new Vector3(42, 80, 0), new Item(GetItemModel("OakTimber")), ItemPrefab);
        }

        public void SpawnItem(Vector2 position, string itemName)
        {
            ItemWorld.SpawnItemWorld((Vector3)position, new Item(GetItemModel(itemName)), ItemPrefab);
        }

        public List<GameObject> SpawnProps(List<Vector2Int> positions, List<GameObject> prefabs, int minimalObjects, int maximumObjects)
        {
            List<GameObject> props = new List<GameObject>();

            for(int i = 0; i < prefabs.Count; i++)
            {
                for(int j = minimalObjects; j < Random.Range(minimalObjects, maximumObjects); j++)
                {
                    Vector2Int spawnPosition = positions[Random.Range(0, positions.Count - 1)];
                    RaycastHit2D spawnPositionCheck = Physics2D.Raycast(spawnPosition, Vector2.zero);

                    if(spawnPositionCheck.collider != null)
                    {
                        j--;
                        continue;
                    }

                    props.Add(SpawnProp(spawnPosition, prefabs[i]));
                }
            }

            return props;
        }

        public GameObject SpawnProp(Vector2 position, GameObject prefab)
        {
            Transform prop = Instantiate(prefab.transform, new Vector3(position.x, position.y + prefab.transform.localScale.x * 0.65f ), Quaternion.identity);
            return prop.gameObject;
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
