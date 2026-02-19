using System;
using Project.Scripts.SceneManagementSystem;
using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private UIController uiController;

        private bool _isInMinigame;

        private void OnMinigameTriggered(GameScene minigame)
        {
            _isInMinigame = true;
            uiController.HideAll();
            inputReader.DisableAllInput();
            SceneTransitionPlan.Create()
                .Load(minigame, true)
                .Perform();
        }

        void OnMinigameEnded()
        {
            print("Called OnMinigameEnded");
            _isInMinigame = false;
            uiController.ShowHud();
            inputReader.EnablePlayerInput();
        }

        void OnDialogueStarted()
        {
            inputReader.EnableDialogueInput();
            uiController.ShowDialogue();
        }

        void OnDialogueEnded()
        {
            if (_isInMinigame)
                return;
            inputReader.EnablePlayerInput();
            uiController.ShowHud();
        }

        private void OnEnable()
        {
            inputReader.EnablePlayerInput();
            EventBus.MinigameTriggered += OnMinigameTriggered;
            EventBus.MinigameEnded += OnMinigameEnded;
            EventBus.DialogueStarted += OnDialogueStarted;
            EventBus.DialogueEnded += OnDialogueEnded;
        }

        private void OnDisable()
        {
            inputReader.DisableAllInput();
            EventBus.MinigameTriggered -= OnMinigameTriggered;
            EventBus.MinigameEnded -= OnMinigameEnded;
            EventBus.DialogueStarted -= OnDialogueStarted;
            EventBus.DialogueEnded -= OnDialogueEnded;
        }
    }
}