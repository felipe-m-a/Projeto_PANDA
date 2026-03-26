using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Spaceship
{
    public class Obstacle : MonoBehaviour
    {
        private const float Speed = 8f;
        private float destroyLocation;

        private void Start()
        {
            destroyLocation = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
        }

        private void Update()
        {
            transform.position += Vector3.left * (Speed * Time.deltaTime);

            if (transform.position.x < destroyLocation) Destroy(gameObject);
        }

        public void Initialize(Vector3 position)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }
}