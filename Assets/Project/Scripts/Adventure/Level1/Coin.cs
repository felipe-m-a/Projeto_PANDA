using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private StoryTracker storyTracker;

        private void Update()
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() is null) return;
            storyTracker.collectedCoinsCount++;
            gameObject.SetActive(false);
        }
    }
}