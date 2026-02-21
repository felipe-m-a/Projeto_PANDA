using System;
using Project.Scripts.SceneManagementSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.Menu
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private Settings settings;

        private DropdownField _difficultyDropdown;

        private VisualElement _minigamesPage;
        private VisualElement _settingsPage;
        private Toggle _showOnScreenControlsToggle;
        private VisualElement _titlePage;


        private void Start()
        {
            var root = document.rootVisualElement;

            _titlePage = root.Q<VisualElement>("TitlePage");
            _minigamesPage = root.Q<VisualElement>("MinigamesPage");
            _settingsPage = root.Q<VisualElement>("SettingsPage");

            // Title page
            _titlePage.Q<Button>("AdventureButton").RegisterCallback<ClickEvent>(OnTitlePageAdventureButtonClicked);
            _titlePage.Q<Button>("MinigamesButton").RegisterCallback<ClickEvent>(OnTitlePageMinigamesButtonClicked);
            _titlePage.Q<Button>("SettingsButton").RegisterCallback<ClickEvent>(OnTitlePageSettingsButtonClicked);

            // Minigames page
            _minigamesPage.Q<Button>("BackButton").RegisterCallback<ClickEvent>(OnMinigamesPageBackButtonClicked);
            _minigamesPage.Q<Button>("MemoryButton").RegisterCallback<ClickEvent>(OnMinigamesPageMemoryButtonClicked);
            _minigamesPage.Q<Button>("PuzzleButton").RegisterCallback<ClickEvent>(OnMinigamesPagePuzzleButtonClicked);
            _minigamesPage.Q<Button>("FlowButton").RegisterCallback<ClickEvent>(OnMinigamesPageFlowButtonClicked);
            _minigamesPage.Q<Button>("PipesButton").RegisterCallback<ClickEvent>(OnMinigamesPagePipesButtonClicked);
            _minigamesPage.Q<Button>("SpaceshipButton").RegisterCallback<ClickEvent>(OnMinigamesPageSpaceshipButtonClicked);
            _minigamesPage.Q<Button>("WhackButton").RegisterCallback<ClickEvent>(OnMinigamesPageWhackButtonClicked);

            // Settings page
            _settingsPage.Q<Button>("BackButton").RegisterCallback<ClickEvent>(OnSettingsPageBackButtonClicked);

            _difficultyDropdown = _settingsPage.Q<DropdownField>("DifficultyDropdown");
            _difficultyDropdown.index = settings.difficultyIndex;
            _difficultyDropdown.RegisterValueChangedCallback(OnDifficultyDropdownValueChanged);

            _showOnScreenControlsToggle = _settingsPage.Q<Toggle>("ShowOnScreenControlsToggle");
            _showOnScreenControlsToggle.value = settings.showOnScreenControls;
            _showOnScreenControlsToggle.RegisterValueChangedCallback(OnShowOnScreenControlsToggleValueChanged);
        }

        private void OnDisable()
        {
            // Title page
            _titlePage.Q<Button>("AdventureButton").UnregisterCallback<ClickEvent>(OnTitlePageAdventureButtonClicked);
            _titlePage.Q<Button>("MinigamesButton").UnregisterCallback<ClickEvent>(OnTitlePageMinigamesButtonClicked);
            _titlePage.Q<Button>("SettingsButton").UnregisterCallback<ClickEvent>(OnTitlePageSettingsButtonClicked);

            // Minigames page
            _minigamesPage.Q<Button>("BackButton").UnregisterCallback<ClickEvent>(OnMinigamesPageBackButtonClicked);
            _minigamesPage.Q<Button>("MemoryButton").UnregisterCallback<ClickEvent>(OnMinigamesPageMemoryButtonClicked);
            _minigamesPage.Q<Button>("PuzzleButton").UnregisterCallback<ClickEvent>(OnMinigamesPagePuzzleButtonClicked);
            _minigamesPage.Q<Button>("FlowButton").UnregisterCallback<ClickEvent>(OnMinigamesPageFlowButtonClicked);
            _minigamesPage.Q<Button>("PipesButton").UnregisterCallback<ClickEvent>(OnMinigamesPagePipesButtonClicked);
            _minigamesPage.Q<Button>("SpaceshipButton").UnregisterCallback<ClickEvent>(OnMinigamesPageSpaceshipButtonClicked);
            _minigamesPage.Q<Button>("WhackButton").UnregisterCallback<ClickEvent>(OnMinigamesPageWhackButtonClicked);

            // Settings page
            _settingsPage.Q<Button>("BackButton").UnregisterCallback<ClickEvent>(OnSettingsPageBackButtonClicked);
            _difficultyDropdown.UnregisterValueChangedCallback(OnDifficultyDropdownValueChanged);
            _showOnScreenControlsToggle.UnregisterValueChangedCallback(OnShowOnScreenControlsToggleValueChanged);
        }

        private static void SubscribeButton(VisualElement root, string name, Action action)
        {
            var button = root.Q<Button>(name);
            if (button != null)
                button.clicked += action;
        }

        private static void UnsubscribeButton(VisualElement root, string name, Action action)
        {
            var button = root.Q<Button>(name);
            if (button != null)
                button.clicked -= action;
        }

        #region Title page

        private void OnTitlePageAdventureButtonClicked(ClickEvent evt)
        {
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Menu)
                .Load(GameScene.AdventureLevel1, true)
                .Perform();
        }

        private void OnTitlePageMinigamesButtonClicked(ClickEvent evt)
        {
            _titlePage.style.display = DisplayStyle.None;
            _minigamesPage.style.display = DisplayStyle.Flex;
        }

        private void OnTitlePageSettingsButtonClicked(ClickEvent evt)
        {
            _titlePage.style.display = DisplayStyle.None;
            _settingsPage.style.display = DisplayStyle.Flex;
        }

        #endregion

        #region Minigames page

        private void OnMinigamesPageBackButtonClicked(ClickEvent evt)
        {
            _minigamesPage.style.display = DisplayStyle.None;
            _titlePage.style.display = DisplayStyle.Flex;
        }

        private void OnMinigamesPageMemoryButtonClicked(ClickEvent evt)
        {
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Menu)
                .Load(GameScene.MinigameMemory, true)
                .Perform();
        }

        private void OnMinigamesPageFlowButtonClicked(ClickEvent evt)
        {
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Menu)
                .Load(GameScene.MinigameFlow, true)
                .Perform();
        }

        private void OnMinigamesPagePuzzleButtonClicked(ClickEvent evt)
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigamePuzzle, true)
            //     .Perform();
        }

        private void OnMinigamesPagePipesButtonClicked(ClickEvent evt)
        {
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Menu)
                .Load(GameScene.MinigamePipes, true)
                .Perform();
        }

        private void OnMinigamesPageSpaceshipButtonClicked(ClickEvent evt)
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameSpaceship, true)
            //     .Perform();
        }

        private void OnMinigamesPageWhackButtonClicked(ClickEvent evt)
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameWhack, true)
            //     .Perform();
        }

        #endregion

        #region Settings page

        private void OnSettingsPageBackButtonClicked(ClickEvent evt)
        {
            _settingsPage.style.display = DisplayStyle.None;
            _titlePage.style.display = DisplayStyle.Flex;
        }

        private void OnDifficultyDropdownValueChanged(ChangeEvent<string> evt)
        {
            settings.difficultyIndex = _difficultyDropdown.choices.IndexOf(evt.newValue);
        }

        private void OnShowOnScreenControlsToggleValueChanged(ChangeEvent<bool> evt)
        {
            settings.showOnScreenControls = evt.newValue;
        }

        #endregion
    }
}