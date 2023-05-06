using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerCharacterManager: MonoBehaviour
    {
        // Stats
        // Inventory

        [SerializeField] private int _maxInventorySlots = 10;

        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory(_maxInventorySlots);
            Debug.Log("Inventory created");
            // set ui inventory
        }

        private void Start()
        {
            ItemSpawner.Instance.SpawnItems();
        }
    }
}
