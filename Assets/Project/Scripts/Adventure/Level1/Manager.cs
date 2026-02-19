using Project.Scripts.SceneManagementSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private UIController uiController;
        [SerializeField] private StoryTracker storyTracker;

        private bool _isInMinigame;

        private void OnEnable()
        {
            inputReader.EnablePlayerInput();
            storyTracker.CompletedEvent += OnCompletedEvent;
            EventBus.MinigameTriggered += OnMinigameTriggered;
            EventBus.MinigameEnded += OnMinigameEnded;
            EventBus.DialogueStarted += OnDialogueStarted;
            EventBus.DialogueEnded += OnDialogueEnded;
        }

        private void OnDisable()
        {
            inputReader.DisableAllInput();
            storyTracker.CompletedEvent -= OnCompletedEvent;
            EventBus.MinigameTriggered -= OnMinigameTriggered;
            EventBus.MinigameEnded -= OnMinigameEnded;
            EventBus.DialogueStarted -= OnDialogueStarted;
            EventBus.DialogueEnded -= OnDialogueEnded;
        }

        private void OnMinigameTriggered(GameScene minigame)
        {
            _isInMinigame = true;
            uiController.HideAll();
            inputReader.DisableAllInput();
            SceneTransitionPlan.Create()
                .Load(minigame, true)
                .Perform();
        }

        private void OnMinigameEnded()
        {
            _isInMinigame = false;
            uiController.ShowHud();
            inputReader.EnablePlayerInput();
        }

        private void OnDialogueStarted()
        {
            inputReader.EnableDialogueInput();
            uiController.ShowDialogue();
        }

        private void OnDialogueEnded()
        {
            if (_isInMinigame)
                return;
            inputReader.EnablePlayerInput();
            uiController.ShowHud();
        }

        private void OnCompletedEvent()
        {
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Adventure)
                .Load(GameScene.Menu, true)
                .Perform();
        }
    }
}