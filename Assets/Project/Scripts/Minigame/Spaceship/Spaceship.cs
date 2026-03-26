using System;
using UnityEngine;
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
        private Rigidbody2D _rigidbody;
        private Vector3 _startPosition;

        private void Start()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody2D>();
            _startPosition = _rigidbody.position;

            pressInput.action.performed += OnPressInput;
            pressInput.action.canceled += OnPressInput;
        }


        private void Update()
        {
            if (_isPressed)
            {
                var screenPosition = positionInput.action.ReadValue<Vector2>();
                var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
                var moveDirection = (worldPosition - transform.position).normalized;

                _rigidbody.linearVelocity = moveDirection * maxSpeed;
            }
            else
            {
                _rigidbody.linearVelocity = Vector3.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle")) CollidedEvent?.Invoke();
        }

        public event Action CollidedEvent;

        public void Restart()
        {
            _isPressed = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.position = _startPosition;
        }

        private void OnPressInput(InputAction.CallbackContext context)
        {
            _isPressed = context.ReadValue<float>() > 0.5f;
        }
    }
}