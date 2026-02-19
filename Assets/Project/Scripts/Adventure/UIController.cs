using System;
using Project.Scripts.SceneManagementSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.Adventure
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private Canvas onScreenControls;
        [SerializeField] private Settings settings;

        private Label _coinsLabel;
        private Label _dialogueLabel;

        private VisualElement _dialoguePanel;

        private VisualElement _hudPanel;

        private VisualElement _pauseMenu;

        private void Start()
        {
            var root = document.rootVisualElement;

            // Hud
            _hudPanel = root.Q<VisualElement>("HudPanel");
            _coinsLabel = _hudPanel.Q<Label>("CoinsLabel");
            _hudPanel.Q<Button>("PauseButton").RegisterCallback<ClickEvent>(OnPauseButtonClicked);

            // Dialogue
            _dialoguePanel = root.Q<VisualElement>("DialoguePanel");
            _dialoguePanel.RegisterCallback<ClickEvent>(OnDialoguePanelClicked);
            _dialogueLabel = _dialoguePanel.Q<Label>("DialogueLabel");

            // Pause menu
            _pauseMenu = root.Q<VisualElement>("PauseMenu");
            _pauseMenu.Q<Button>("ResumeButton").RegisterCallback<ClickEvent>(OnResumeButtonClicked);
            _pauseMenu.Q<Button>("QuitButton").RegisterCallback<ClickEvent>(OnQuitButtonClicked);
        }

        public event Action DialogueClicked;

        public void UpdateCoins(string text)
        {
            _coinsLabel.text = text;
        }

        public void UpdateDialogueText(string text)
        {
            _dialogueLabel.text = text;
        }

        public void ShowDialogue()
        {
            HideOnscreenControls();

            _hudPanel.style.display = DisplayStyle.None;
            _dialoguePanel.style.display = DisplayStyle.Flex;
        }

        public void HideDialogue()
        {
            _dialoguePanel.style.display = DisplayStyle.None;
            _hudPanel.style.display = DisplayStyle.Flex;

            ShowOnscreenControls();
        }

        public void ShowOnscreenControls()
        {
            if (settings.showOnScreenControls)
                onScreenControls.enabled = true;
        }

        public void HideOnscreenControls()
        {
            onScreenControls.enabled = false;
        }

        private void OnPauseButtonClicked(ClickEvent evt)
        {
            HideOnscreenControls();
            Time.timeScale = 0;
            _hudPanel.style.display = DisplayStyle.None;
            _pauseMenu.style.display = DisplayStyle.Flex;
        }

        private void OnDialoguePanelClicked(ClickEvent evt)
        {
            DialogueClicked?.Invoke();
        }

        private void OnQuitButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1;
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Minigame)
                .Unload(GameScene.SceneType.Adventure)
                .Load(GameScene.Menu, true)
                .Perform();
        }

        private void OnResumeButtonClicked(ClickEvent evt)
        {
            ShowOnscreenControls();
            Time.timeScale = 1;
            _pauseMenu.style.display = DisplayStyle.None;
            _hudPanel.style.display = DisplayStyle.Flex;
        }
    }
}