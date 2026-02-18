using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Adventure
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/Adventure/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Player.SetCallbacks(this);
            }

            DisableAllInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                InteractEvent?.Invoke();
        }

        // Adventure
        public event Action<Vector2> MoveEvent;
        public event Action InteractEvent;

        public void DisableMoveInput()
        {
            _gameInput.Player.Move.Disable();
        }

        public void EnableAllInput()
        {
            _gameInput.Player.Enable();
        }

        public void DisableAllInput()
        {
            _gameInput.Player.Disable();
        }
    }
}