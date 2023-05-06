﻿using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemWorld: MonoBehaviour
    {
        private Item _item;

        private SpriteRenderer _itemSpriteRenderer;

        public void Awake()
        {
            _itemSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetItem(Item item)
        {
            _item = item;
            _itemSpriteRenderer.sprite = _item.ItemSprite;
        }

        public Item GetItem() => _item;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public static ItemWorld SpawnItemWorld(Vector3 position, Item item, GameObject itemPrefab)
        {
            Transform transform = Instantiate(itemPrefab.transform, position, Quaternion.identity);

            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            itemWorld.SetItem(item);

            return itemWorld;
        }
    }
}