using Assets.Scripts.Player;
using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInteractionController;

namespace Assets.Scripts
{
    public class InventoryUI : MonoBehaviour
    {
        // Manage click events (swap items, show/close extended inventory)
        // Refresh items

        private Transform _standartInventoryObject;
        private Transform _standartInventoryCellContainer;
        private Transform _standartInventoryHighlightCell;
        private List<Transform> _standartInventoryCells;

        private Transform _extendedInventorySlotTemplate;
        private Transform _extendedInventoryObject;
        private Transform _extendedInventoryArea;
        private Transform _extendedInventoryItemsArea;
        private Transform _extendedInventoryCreateArea;

        private Inventory _characterInventory;

        private void Awake()
        {
            StandartInventoryInitialize();
            ExtendedInventoryInitialize();
            
            var playerController = FindObjectOfType<PlayerInteractionController>();

            if(playerController == null)
            {
                Debug.LogError("Player interaction script not found in UI script");
                
                return;
            }

            playerController.OnNumberKeyPressed += PlayerInteractionController_OnNumberKeyPressed;
            playerController.OnExtendedInventoryKeyPressed += OnExtendedInventoryKeyPressed_CallExtendedInventory;
        }

        public void SetInventory(Inventory inventory)
        {
            _characterInventory = inventory;
            _characterInventory.OnItemListChanged += Inventory_OnItemListChanged;

            _standartInventoryHighlightCell.position = _standartInventoryCells[0].position;
            UpdateStandartInventoryUI();
        }

        private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
        {
            UpdateStandartInventoryUI();
            UpdateExtendedInventoryUI();
        }

        private void PlayerInteractionController_OnNumberKeyPressed(object sender, NumberKeyPressedEventArgs e)
        {
            int pressedNumber = e.number;

            SwitchItem(pressedNumber);
        }

        private void OnExtendedInventoryKeyPressed_CallExtendedInventory(object sender, EventArgs e)
        {
            bool standartInventoryIsActive = _standartInventoryObject.gameObject.activeSelf;
            bool extenedInventoryIsActive = _extendedInventoryObject.gameObject.activeSelf;

            _standartInventoryObject.gameObject.SetActive(!standartInventoryIsActive);
            _extendedInventoryObject.gameObject.SetActive(!extenedInventoryIsActive);

            UpdateStandartInventoryUI();
            UpdateExtendedInventoryUI();
        }

        private void UpdateExtendedInventoryUI() 
        {
            if (!_extendedInventoryObject.gameObject.activeSelf)
            {
                return;
            }

            foreach (Transform child in _extendedInventoryItemsArea)
            {
                if(child == _extendedInventorySlotTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }

            RectTransform slotTemplateRectTransform = _extendedInventorySlotTemplate.GetComponent<RectTransform>();
            float startX = slotTemplateRectTransform.anchoredPosition.x;
            float startY = slotTemplateRectTransform.anchoredPosition.y;
            
            int offsetX = 0;
            int offsetY = 0;

            float itemSlotCellSize = slotTemplateRectTransform.sizeDelta.x;

            foreach(var itemSlot in _characterInventory.InventorySlots)
            {
                RectTransform itemSlotRectTransform = Instantiate(_extendedInventorySlotTemplate, _extendedInventoryItemsArea).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(startX + offsetX * itemSlotCellSize, startY + offsetY * itemSlotCellSize);
                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {

                };

                itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    Debug.Log("Clicked");
                    
                    if(itemSlot.Item == null)
                    {
                        return;
                    }

                    InventorySlot duplicateItem = new InventorySlot();

                    ItemWorld.DropItem(itemSlot.Item, itemSlot.Quantity);
                    _characterInventory.RemoveItem(itemSlot.Item);
                };


                Image slotImage = itemSlotRectTransform.Find("ItemSlotHolder_Object").GetComponent<Image>();

                if (itemSlot.Item == null)
                {
                    slotImage.gameObject.SetActive(false);
                }
                else
                {
                    slotImage.gameObject.SetActive(true);
                    slotImage.sprite = itemSlot.Item.ItemSprite;
                    // Quantity
                }

                TextMeshProUGUI uiText = itemSlotRectTransform.Find("ItemSlotQuantity_Object").GetComponent<TextMeshProUGUI>();
                uiText.text = "";

                if (itemSlot.Quantity > 1)
                {
                    uiText.text = itemSlot.Quantity.ToString();
                }

                offsetX++;

                if(offsetX > 9)
                {
                    offsetX = 0;
                    offsetY--;
                }
            }

            return;
        }

        private void UpdateStandartInventoryUI()
        {
            if(!_standartInventoryObject.gameObject.activeSelf)
            {
                return;
            }

            List<InventorySlot> inventorySlots = _characterInventory.InventorySlots.GetRange(0, 10);

            for(int i = 0; i < _standartInventoryCells.Count; i++)
            {
                if (inventorySlots[i].IsEmpty())
                {
                    ResetItemInStandartCell(_standartInventoryCells[i]);
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
            Transform cellQuantity = cell.Find("Cell_Item_Quantity");
            cellItem.gameObject.SetActive(false);
            cellQuantity.gameObject.SetActive(false);
        }

        private void SwitchItem(int cellNumber)
        {
            _characterInventory.ChangeCurrentSlot(cellNumber);

            _standartInventoryHighlightCell.position = _standartInventoryCells[cellNumber - 1].position;
        }

        private void StandartInventoryInitialize()
        {
            _standartInventoryObject = transform.Find("StandartInventory_Object");
            _standartInventoryHighlightCell = _standartInventoryObject.Find("Selected_Cell_Highlight");
            _standartInventoryCellContainer = _standartInventoryObject.Find("Inventory_Cells");
            _standartInventoryCells = new List<Transform>();

            foreach (Transform cell in _standartInventoryCellContainer)
            {
                _standartInventoryCells.Add(cell);
            }

            _standartInventoryObject.gameObject.SetActive(true);
        }

        private void ExtendedInventoryInitialize()
        {
            _extendedInventoryObject = transform.Find("ExtendedInventory_Object");
            _extendedInventoryArea = _extendedInventoryObject.Find("Inventory_Area");
            _extendedInventoryItemsArea = _extendedInventoryArea.Find("InventoryItems_Area");
            _extendedInventorySlotTemplate = _extendedInventoryItemsArea.Find("ItemSlotTemplate_Object");
            _extendedInventoryArea = _extendedInventoryObject.Find("InventoryCreate_Area");

            _extendedInventoryObject.gameObject.SetActive(false);
        }
    }
}