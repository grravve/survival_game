using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerCharacterManager: MonoBehaviour
    {
        // Stats
        // Inventory

        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private int _maxInventorySlots = 25;

        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(_maxInventorySlots);
            //Inventory.AddItem(new Item(ItemSpawner.Instance.GetItemModel("OakTimber")));
            //Inventory.AddItem(new Item(ItemSpawner.Instance.GetItemModel("BirchTimber")));

            _inventoryUI.SetInventory(Inventory);
            Debug.Log("UI set inventory");
        }

        private void Start()
        {
            ItemSpawner.Instance.SpawnItems();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ItemWorld item = collision.GetComponent<ItemWorld>();

            if (item == null)
            {
                return;
            }

            bool isAdded = Inventory.AddItem(item.GetItem());

            if (isAdded)
            {
                item.DestroySelf();
            }

            Debug.Log("Item added");
        }
    }
}
