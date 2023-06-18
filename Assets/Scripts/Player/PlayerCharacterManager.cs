using Assets.Scripts.Player;
using UnityEngine;
using static Assets.Scripts.PlayerMovementController;

namespace Assets.Scripts
{
    class PlayerCharacterManager: MonoBehaviour
    {
        // Stats
        // Inventory

        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private int _maxInventorySlots = 30;

        public Inventory Inventory { get; private set; }
       
        private void Awake()
        {
            Inventory = new Inventory(_maxInventorySlots);
            Inventory.AddItem(new Axe(ObjectSpawner.Instance.GetItemModel("Iron Axe"), 1.2f), 1);
            //Inventory.AddItem(new Item(ItemSpawner.Instance.GetItemModel("BirchTimber")));
            GetComponent<PlayerMovementController>().OnItemPickedUp += OnItemPickedUp;
        }

        private void Start()
        {
            _inventoryUI.SetInventory(Inventory);
            //Debug.Log("UI set inventory");
            ObjectSpawner.Instance.SpawnItems();
        }

        private void OnItemPickedUp(object sender, ItemPickedUpEventArgs e)
        {
            ItemWorld item = e.pickedUpItem;

            bool isAdded = Inventory.AddItem(item.GetItem(), item.GetQuantity);

            if (isAdded)
            {
                item.DestroySelf();
            }
        }
    }
}
