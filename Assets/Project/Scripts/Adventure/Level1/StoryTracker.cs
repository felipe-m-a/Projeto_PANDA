using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class StoryTracker : MonoBehaviour
    {
        [SerializeField] private UIController uiController;

        public bool receivedQuest;
        public bool receivedParts;

        public int CollectedCoinsCount { get; private set; }

        public void AddCoin()
        {
            CollectedCoinsCount++;
            uiController.UpdateCoins(CollectedCoinsCount.ToString());
        }
    }
}