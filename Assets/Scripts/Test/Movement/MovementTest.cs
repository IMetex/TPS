using System;
using TPS.Input;
using TPS.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Test.Movement
{
    public class MovementTest : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;

        [SerializeField] private Vector3 _extaranalForce;
        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Dodge.performed += OnDodgeButtonPressed;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Dodge.performed -= OnDodgeButtonPressed;
        }

        private void OnDodgeButtonPressed(InputAction.CallbackContext obj)
        {
            _characterMovement.ExternalForces += _extaranalForce;
        }

        private void Update()
        {
            var input = _gameInput.Player.Movement.ReadValue<Vector2>();
            _characterMovement.MovementInput = input;
        }
    }
}