using System;
using Project.Scripts.SceneManagementSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class StoryTracker : MonoBehaviour
    {
        [SerializeField] private UIController uiController;

        public bool receivedQuest;
        public bool deliveredCoins;
        public bool receivedParts;
        public bool completedFirstMinigame;
        public bool deliveredParts;
        public bool completedSecondMinigame;

        public int CollectedCoinsCount { get; private set; }

        public void AddCoin()
        {
            CollectedCoinsCount++;
            uiController.UpdateCoins(CollectedCoinsCount.ToString());
        }

        public void SubtractCoins(int amount)
        {
            CollectedCoinsCount -= amount;
            uiController.UpdateCoins(CollectedCoinsCount.ToString());
        }

        private void OnEnable()
        {
            EventBus.DialogueEnded += OnDialogueEnded;
        }

        private void OnDialogueEnded()
        {
            if (deliveredCoins && !completedFirstMinigame)
            {
                EventBus.TriggerMinigame(GameScene.MinigameMemory);
                completedFirstMinigame = true;
            }

            if (deliveredParts && !completedSecondMinigame)
            {
                EventBus.TriggerMinigame(GameScene.MinigameFlow);
                completedSecondMinigame = true;
            }
        }
    }
}