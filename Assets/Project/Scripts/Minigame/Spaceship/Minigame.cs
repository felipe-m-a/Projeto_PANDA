using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Spaceship
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Obstacle obstaclePrefab;
        [SerializeField] private Spaceship spaceship;
        [SerializeField] private float spawnLocationMinY = -3.5f;
        [SerializeField] private float spawnLocationMaxY = 2f;

        private float _duration = 20f;
        private float _elapsedTime;
        private bool _finished;
        private float _spawnDelay = 2f;

        private float _spawnLocationX;

        private void Start()
        {
            _duration = settings.CurrentDifficultySettings.minigameSpaceshipDuration;
            _spawnDelay = settings.CurrentDifficultySettings.minigameSpaceshipSpawnDelay;

            _spawnLocationX = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x + 3f;
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime < _duration) return;

            _finished = true;
            DestroyAllObstacles();
            SolvedEvent?.Invoke();
        }

        private void OnEnable()
        {
            spaceship.CollidedEvent += Restart;
            Invoke(nameof(Spawn), _spawnDelay);
        }

        private void OnDisable()
        {
            spaceship.CollidedEvent -= Restart;
        }

        public event Action SolvedEvent;


        private void Spawn()
        {
            if (_finished) return;

            var obstacle = Instantiate(obstaclePrefab);
            obstacle.Initialize(new Vector3(_spawnLocationX, Random.Range(spawnLocationMinY, spawnLocationMaxY)));

            Invoke(nameof(Spawn), _spawnDelay);
        }

        private void Restart()
        {
            DestroyAllObstacles();
            spaceship.Restart();
            _elapsedTime = 0f;
        }

        private void DestroyAllObstacles()
        {
            foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle")) Destroy(obstacle);
        }
    }
}