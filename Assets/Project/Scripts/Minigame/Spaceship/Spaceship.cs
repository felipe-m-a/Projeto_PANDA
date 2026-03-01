using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Project.Scripts.Minigame.Spaceship
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private InputActionReference positionInput;
        [SerializeField] private InputActionReference pressInput;
        [SerializeField] private float maxSpeed = 80f;

        private Camera _camera;

        private bool _isPressed;

        private void Start()
        {
            _camera = Camera.main;

            positionInput.action.performed += OnPositionInput;
            pressInput.action.performed += OnPressInput;
            pressInput.action.canceled += OnPressInput;
        }

        private void OnPressInput(InputAction.CallbackContext context)
        {
            _isPressed = context.ReadValue<float>() > 0.5f;
        }

        private void OnPositionInput(InputAction.CallbackContext context)
        {
            if (!_isPressed) return;
            var screenPosition = context.ReadValue<Vector2>();
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);

            transform.position = Vector2.MoveTowards(transform.position, worldPosition, maxSpeed * Time.deltaTime);
        }
    }
}