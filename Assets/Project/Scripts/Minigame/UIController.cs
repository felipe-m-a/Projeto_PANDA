using Project.Scripts.SceneManagementSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.Minigame
{
    [RequireComponent(typeof(UIDocument))]
    public class UIController : MonoBehaviour
    {
        [SerializeField] private string title;

        private VisualElement _congratulationsMenu;
        private VisualElement _header;
        private VisualElement _pauseMenu;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            // Header
            _header = root.Q<VisualElement>("Header");
            _header.Q<Label>("Title").text = title;
            _header.Q<Button>("PauseButton").RegisterCallback<ClickEvent>(OnPauseButtonClicked);

            // Pause menu
            _pauseMenu = root.Q<VisualElement>("PauseMenu");
            _pauseMenu.Q<Button>("ResumeButton").RegisterCallback<ClickEvent>(OnPauseMenuResumeButtonClicked);
            _pauseMenu.Q<Button>("QuitButton").RegisterCallback<ClickEvent>(OnPauseMenuQuitButtonClicked);

            // Congratulations menu
            _congratulationsMenu = root.Q<VisualElement>("CongratulationsMenu");
            _congratulationsMenu.Q<Button>("ResumeButton").RegisterCallback<ClickEvent>(OnCongratulationsMenuResumeButtonClicked);
        }

        private void OnDisable()
        {
            // Header
            _header.Q<Button>("PauseButton").UnregisterCallback<ClickEvent>(OnPauseButtonClicked);

            // Pause menu
            _pauseMenu.Q<Button>("ResumeButton").UnregisterCallback<ClickEvent>(OnPauseMenuResumeButtonClicked);
            _pauseMenu.Q<Button>("QuitButton").UnregisterCallback<ClickEvent>(OnPauseMenuQuitButtonClicked);

            // Congratulations menu
            _congratulationsMenu.Q<Button>("ResumeButton").UnregisterCallback<ClickEvent>(OnCongratulationsMenuResumeButtonClicked);
        }

        public void ShowCongratulationsMenu()
        {
            _congratulationsMenu.style.display = DisplayStyle.Flex;
        }

        private void OnPauseButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 0;
            _pauseMenu.style.display = DisplayStyle.Flex;
        }

        private void OnPauseMenuResumeButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1;
            _pauseMenu.style.display = DisplayStyle.None;
        }

        private void OnPauseMenuQuitButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1;
            SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Minigame)
                .Unload(GameScene.SceneType.Adventure)
                .Load(GameScene.Menu, true)
                .Perform();
        }

        private void OnCongratulationsMenuResumeButtonClicked(ClickEvent evt)
        {
            var plan = SceneTransitionPlan.Create()
                .Unload(GameScene.SceneType.Minigame);

            if (!SceneController.Instance.HasLoaded(GameScene.SceneType.Adventure))
                plan.Load(GameScene.Menu, true);

            plan.Perform();
        }
    }
}