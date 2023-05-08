﻿using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryUI : MonoBehaviour
    {
        // Manage click events (swap items, show/close extended inventory)
        // Refresh items

        private Transform _standartInventoryObject;
        private Transform _standartInventoryCellContainer;
        private List<Transform> _standartInventoryCells;

        private GameObject _extendedInventoryObject;

        private Inventory _characterInventory;

        private void Awake()
        {
            _standartInventoryObject = transform.Find("Standart_Inventory_Object");
            _standartInventoryCellContainer = _standartInventoryObject.Find("Inventory_Cells");
            _standartInventoryCells = new List<Transform>();

            foreach (Transform cell in _standartInventoryCellContainer)
            {
                _standartInventoryCells.Add(cell);
            }
        }

        public void SetInventory(Inventory inventory)
        {
            _characterInventory = inventory;
            _characterInventory.OnItemAdded += Inventory_OnItemAdded;
            
            RefreshStandartInventoryUI();
        }

        private void Inventory_OnItemAdded(object sender, System.EventArgs e)
        {
            RefreshStandartInventoryUI();
        }

        public void RefreshStandartInventoryUI()
        {
            List<InventorySlot> inventorySlots = _characterInventory.InventorySlots.GetRange(0, 10);

            for(int i = 0; i < _standartInventoryCells.Count; i++)
            {
                if (inventorySlots[i].IsEmpty())
                {
                    continue;
                }

                SetItemInStandartCell(_standartInventoryCells[i], inventorySlots[i]);
            }
        }

        private void SetItemInStandartCell(Transform cell, InventorySlot itemSlot)
        {
            Transform cellItem = cell.Find("Cell_Item");
            Transform cellItemQuantityText = cell.Find("Cell_Item_Quantity");
            cellItem.gameObject.SetActive(true);
            cellItem.GetComponent<Image>().sprite = itemSlot.Item.ItemSprite;
            
            if(itemSlot.Quantity > 1)
            {
                cellItemQuantityText.gameObject.SetActive(true);
                cellItemQuantityText.GetComponent<TextMeshProUGUI>().SetText(itemSlot.Quantity.ToString());
            }
            else
            {
                cellItemQuantityText.gameObject.SetActive(false);
            }

        }

        private void ResetItemInStandartCell(Transform cell)
        {
            Transform cellItem = cell.Find("Cell_Item");
            cellItem.GetComponent<Image>().sprite = null;
            cellItem.gameObject.SetActive(false);
        }
    }
}