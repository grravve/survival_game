using Assets.Scripts;
using Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float _minHoldingTime;

    private float _clickTime, _holdingTime;
    private bool _isExtracting;
    private Inventory _characterInventory;

    public EventHandler<NumberKeyPressedEventArgs> OnNumberKeyPressed;
    public class NumberKeyPressedEventArgs: EventArgs
    {
        public int number;
    }

    private void Start()
    {
        _clickTime = 0f;
        _holdingTime = 0f;
        _isExtracting = false;
        _characterInventory = GetComponent<PlayerCharacterManager>().Inventory;
    }

    private void Update()
    {
        HandleClick();
        HandleNumbersPress();
    }

    private void HandleNumbersPress()
    {
        if (Input.inputString != "")
        {
            int inputNumber;
            bool parsing = Int32.TryParse(Input.inputString, out inputNumber);

            if(parsing && inputNumber > 0 && inputNumber < 10)
            {
                OnNumberKeyPressed?.Invoke(this, new NumberKeyPressedEventArgs { number = inputNumber });
            }
        }
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickTime = Time.time;
        }

        if(Input.GetMouseButton(0))
        {
            _holdingTime = Time.time - _clickTime;

            if(_holdingTime < _minHoldingTime)
            {
                // One click logic
                return;
            }

            // Get extractable object data
            // Extract action
            if(!_isExtracting)
            {
                StartCoroutine(ExtractObject());
            }
            // Animations
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Released");
            StopCoroutine(ExtractObject());
            _holdingTime = 0f;
        }
    }

    private IEnumerator ExtractObject()
    {
        _isExtracting = true;
        yield return new WaitForSeconds(1);
        HandleObjectExtraction();
        _isExtracting = false;
    }

    private void HandleObjectExtraction()
    {
        RaycastHit2D clickedObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (clickedObject.collider == null)
        {
            return;
        }

        IExtractable extractedObject = clickedObject.collider.GetComponent<IExtractable>();

        if(extractedObject == null)
        {
            return;
        }

        // Extraction item check


        if(!extractedObject.CanExtract(_characterInventory.CurrentSlot.Item))
        {
            return;
        }

        extractedObject.Extract();
        Debug.Log("Hit");
    }
}
