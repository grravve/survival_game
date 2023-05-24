using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        public float MoveSpeed { get; private set; } = 3f;

        public event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;
        public class ItemPickedUpEventArgs : EventArgs
        {
            public ItemWorld pickedUpItem;
        }

        private Rigidbody2D _playerRigidBody;
        private Animator _playerAnimator;

        private Vector2 _movementInputDirection;

        private bool _isWalking = false;

        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovementInput();
            CheckMovementDirection();
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void HandleMovementInput()
        {
            _movementInputDirection.x = Input.GetAxisRaw("Horizontal");
            _movementInputDirection.y = Input.GetAxisRaw("Vertical");
        }

        private void CheckMovementDirection()
        {
            if(_movementInputDirection.x != 0 || _movementInputDirection.y != 0)
            {
                _isWalking = true;
            }
            else 
            {
                _isWalking = false;
            }

        }

        private void UpdateAnimations()
        {
            _playerAnimator.SetFloat("HorizontalInput", _movementInputDirection.x);
            _playerAnimator.SetFloat("VerticalInput", _movementInputDirection.y);
            _playerAnimator.SetBool("isWalking", _isWalking);
        }

        private void ApplyMovement()
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + _movementInputDirection * MoveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ItemWorld item = collision.GetComponent<ItemWorld>();

            if (item == null)
            {
                return;
            }

            OnItemPickedUp?.Invoke(this, new ItemPickedUpEventArgs { pickedUpItem = item });
        }
    }
}