using System;
using System.Collections;
using System.Collections.Generic;
using TPS.Input;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

namespace TPS.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _characterController;

        public Vector3 ExternalForces { get; set; }

        public Vector2 MovementInput { get; set; }

        private GameInput _gameInput;

        [SerializeField] private float _moveSpeed;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _gameInput = new GameInput();
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDogdeButtonPressed;
        }


        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Dodge.performed -= OnDogdeButtonPressed;
        }

        private void OnDogdeButtonPressed(InputAction.CallbackContext obj)
        {
            ExternalForces = Vector3.Lerp(ExternalForces, Vector3.zero, 5 * Time.deltaTime);
        }

        private void Update()
        {
            var input = _gameInput.Player.Movement.ReadValue<Vector2>();
            var movement = new Vector3(MovementInput.x, 0f, MovementInput.y);
            _characterController.SimpleMove(movement * _moveSpeed + ExternalForces);
        }
    }
}