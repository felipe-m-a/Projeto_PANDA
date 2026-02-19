using System;
using Project.Scripts.SceneManagementSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class StoryTracker : MonoBehaviour
    {
        [SerializeField] private UIController uiController;

        public bool receivedQuest;
        public bool collectedCoins;
        public bool deliveredCoins;
        public bool receivedParts;
        public bool deliveredParts;
        public bool fixedSpaceship;

        public int CollectedCoinsCount { get; private set; }

        private void OnEnable()
        {
            EventBus.DialogueEnded += OnDialogueEnded;
        }

        public event Action CompletedEvent;

        public void AddCoin()
        {
            CollectedCoinsCount++;
            uiController.UpdateCoins(CollectedCoinsCount.ToString());

            if (CollectedCoinsCount >= 3)
                collectedCoins = true;
        }

        public void SubtractCoins(int amount)
        {
            CollectedCoinsCount -= amount;
            uiController.UpdateCoins(CollectedCoinsCount.ToString());
        }

        private void OnDialogueEnded()
        {
            if (deliveredCoins && !receivedParts)
            {
                EventBus.TriggerMinigame(GameScene.MinigameMemory);
                receivedParts = true;
            }
            else if (deliveredParts && !fixedSpaceship)
            {
                EventBus.TriggerMinigame(GameScene.MinigameFlow);
                fixedSpaceship = true;
            }
            else if (fixedSpaceship)
            {
                CompletedEvent?.Invoke();
            }
        }
    }
}