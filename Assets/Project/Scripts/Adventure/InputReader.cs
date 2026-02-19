using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Adventure
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/Adventure/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IDialogueActions
    {
        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Player.SetCallbacks(this);
                _gameInput.Dialogue.SetCallbacks(this);
            }

            DisableAllInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void OnAdvanceDialogue(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                AdvanceDialogueEvent?.Invoke();
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

        public event Action<Vector2> MoveEvent;
        public event Action InteractEvent;
        public event Action AdvanceDialogueEvent;

        public void DisableAllInput()
        {
            _gameInput.Player.Disable();
            _gameInput.Dialogue.Disable();
        }

        public void EnablePlayerInput()
        {
            _gameInput.Player.Enable();
            _gameInput.Dialogue.Disable();
        }

        public void EnableDialogueInput()
        {
            _gameInput.Dialogue.Enable();
            _gameInput.Player.Disable();
        }
    }
}