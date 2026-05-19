using Project.Scripts.Adventure.InteractionSystem;
using Project.Scripts.SceneManagementSystem;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private UIController uiController;
        [SerializeField] private StoryTracker storyTracker;
        [SerializeField] private Player player;

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

        private void Start()
        {
            StartCoroutine(Introduction());
        }

        private IEnumerator Introduction()
        {
            yield return null; // Pula um frame para poder carregar a UI

            var dialogue = new Dialogue(player.transform)
                .Add("Enquanto viajava pelo espaço, sua nave sofreu uma avaria e realizou um pouso de emergência em um planeta desconhecido.")
                .Add("Encontre uma maneira de consertá‑la para conseguir voltar para casa.");

            EventBus.TriggerDialogue(dialogue);
        }
    }
}