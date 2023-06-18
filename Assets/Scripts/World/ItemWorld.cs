using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemWorld: MonoBehaviour
    {
        private Item _item;

        private SpriteRenderer _itemSpriteRenderer;
        private TextMeshPro _textMeshPro;

        public void Awake()
        {
            _itemSpriteRenderer = GetComponent<SpriteRenderer>();
            _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        }

        public void SetItem(Item item, int quantity)
        {
            _item = item;
            _itemSpriteRenderer.sprite = _item.ItemSprite;
            _textMeshPro.text = quantity.ToString();

            if(quantity < 2)
            {
                _textMeshPro.gameObject.SetActive(false);
            }


        }

        public Item GetItem() => _item;

        public int GetQuantity => Int32.Parse(_textMeshPro.text);

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public static ItemWorld SpawnItemWorld(Vector3 position, Item item, int quantity, GameObject itemPrefab)
        {
            Transform transform = Instantiate(itemPrefab.transform, position, Quaternion.identity);

            transform.position = position;
            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            itemWorld.SetItem(item, quantity);

            return itemWorld;
        }

        public static void DropItem(Item item, int quantity)
        {
            var player = FindObjectOfType<PlayerMovementController>().GetComponent<Transform>();
            var offset = new Vector2(UnityEngine.Random.Range(1, 1.5f), UnityEngine.Random.Range(1, 1.5f));
            offset *= Direction2D.GetRandomDirection();

            var spawnPosition = new Vector2(player.position.x + offset.x, player.position.y + offset.y);

            ObjectSpawner.Instance.SpawnItem(spawnPosition, item.Name, quantity);
        }
    }
}
