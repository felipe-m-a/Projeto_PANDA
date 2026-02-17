using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.Menu
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        private VisualElement _minigamesPage;
        private VisualElement _settingsPage;

        private VisualElement _titlePage;

        private void Start()
        {
            if (document == null) return;
            var root = document.rootVisualElement;

            _titlePage = root.Q<VisualElement>("TitlePage");
            _minigamesPage = root.Q<VisualElement>("MinigamesPage");
            _settingsPage = root.Q<VisualElement>("SettingsPage");

            // Title page
            SubscribeButton(_titlePage, "AdventureButton", OnTitlePageAdventureButtonClicked);
            SubscribeButton(_titlePage, "MinigamesButton", OnTitlePageMinigamesButtonClicked);
            SubscribeButton(_titlePage, "SettingsButton", OnTitlePageSettingsButtonClicked);

            // Minigames page
            SubscribeButton(_minigamesPage, "BackButton", OnMinigamesPageBackButtonClicked);
            SubscribeButton(_minigamesPage, "MemoryButton", OnMinigamesPageMemoryButtonClicked);
            SubscribeButton(_minigamesPage, "PuzzleButton", OnMinigamesPagePuzzleButtonClicked);
            SubscribeButton(_minigamesPage, "FlowButton", OnMinigamesPageFlowButtonClicked);
            SubscribeButton(_minigamesPage, "PipesButton", OnMinigamesPagePipesButtonClicked);
            SubscribeButton(_minigamesPage, "SpaceshipButton", OnMinigamesPageSpaceshipButtonClicked);
            SubscribeButton(_minigamesPage, "WhackButton", OnMinigamesPageWhackButtonClicked);

            // Settings page
            SubscribeButton(_settingsPage, "BackButton", OnSettingsPageBackButtonClicked);
        }

        private void OnDisable()
        {
            if (document == null) return;

            // Title page
            UnsubscribeButton(_titlePage, "AdventureButton", OnTitlePageAdventureButtonClicked);
            UnsubscribeButton(_titlePage, "MinigamesButton", OnTitlePageMinigamesButtonClicked);
            UnsubscribeButton(_titlePage, "SettingsButton", OnTitlePageSettingsButtonClicked);

            // Minigames page
            UnsubscribeButton(_minigamesPage, "BackButton", OnMinigamesPageBackButtonClicked);
            UnsubscribeButton(_minigamesPage, "MemoryButton", OnMinigamesPageMemoryButtonClicked);
            UnsubscribeButton(_minigamesPage, "PuzzleButton", OnMinigamesPagePuzzleButtonClicked);
            UnsubscribeButton(_minigamesPage, "FlowButton", OnMinigamesPageFlowButtonClicked);
            UnsubscribeButton(_minigamesPage, "PipesButton", OnMinigamesPagePipesButtonClicked);
            UnsubscribeButton(_minigamesPage, "SpaceshipButton", OnMinigamesPageSpaceshipButtonClicked);
            UnsubscribeButton(_minigamesPage, "WhackButton", OnMinigamesPageWhackButtonClicked);

            // Settings page
            UnsubscribeButton(_settingsPage, "BackButton", OnSettingsPageBackButtonClicked);
        }

        #region Settings page

        private void OnSettingsPageBackButtonClicked()
        {
            _settingsPage.style.display = DisplayStyle.None;
            _titlePage.style.display = DisplayStyle.Flex;
        }

        #endregion

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

        private void OnTitlePageAdventureButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameMemory, true)
            //     .Perform();
        }

        private void OnTitlePageMinigamesButtonClicked()
        {
            _titlePage.style.display = DisplayStyle.None;
            _minigamesPage.style.display = DisplayStyle.Flex;
        }

        private void OnTitlePageSettingsButtonClicked()
        {
            _titlePage.style.display = DisplayStyle.None;
            _settingsPage.style.display = DisplayStyle.Flex;
        }

        #endregion

        #region Minigames page

        private void OnMinigamesPageBackButtonClicked()
        {
            _minigamesPage.style.display = DisplayStyle.None;
            _titlePage.style.display = DisplayStyle.Flex;
        }

        private void OnMinigamesPageMemoryButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameMemory, true)
            //     .Perform();
        }

        private void OnMinigamesPageFlowButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameFlow, true)
            //     .Perform();
        }

        private void OnMinigamesPagePuzzleButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigamePuzzle, true)
            //     .Perform();
        }

        private void OnMinigamesPagePipesButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigamePipes, true)
            //     .Perform();
        }

        private void OnMinigamesPageSpaceshipButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameSpaceship, true)
            //     .Perform();
        }

        private void OnMinigamesPageWhackButtonClicked()
        {
            // SceneTransitionPlan.Create()
            //     .Unload(GameScene.SceneType.Menu)
            //     .Load(GameScene.MinigameWhack, true)
            //     .Perform();
        }

        #endregion
    }
}