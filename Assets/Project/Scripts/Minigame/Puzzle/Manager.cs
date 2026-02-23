using UnityEngine;

namespace Project.Scripts.Minigame.Puzzle
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private UIController uiController;
        [SerializeField] private Minigame minigame;

        private void OnEnable()
        {
            minigame.SolvedEvent += OnMinigameSolvedEvent;
        }

        private void OnDisable()
        {
            minigame.SolvedEvent -= OnMinigameSolvedEvent;
        }

        private void OnMinigameSolvedEvent()
        {
            uiController.ShowCongratulationsMenu();
        }
    }
}