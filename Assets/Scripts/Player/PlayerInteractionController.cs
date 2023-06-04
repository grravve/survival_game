using Assets.Scripts;
using Assets.Scripts.Player;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float _minHoldingTime;

    private float _startHoldingTime, _holdingTime;

    private void Start()
    {
        _startHoldingTime = 0f;
        _holdingTime = 0f;
    }

    private void Update()
    {
        HandleClick();
        HandleObjectExtraction();
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startHoldingTime = Time.time;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _holdingTime = Time.time - _startHoldingTime;
        }
    }

    private void HandleObjectExtraction()
    {
        if (_holdingTime < _minHoldingTime)
        {
            return;
        }

        RaycastHit2D clickedObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (clickedObject.collider == null)
        {
            return;
        }

        clickedObject.collider.GetComponent<ExtractableObject>().DecreaseStrength(1);

        Debug.Log(_holdingTime + "s");
        Debug.Log("\n-1");

        _holdingTime = 0;
    }
}
