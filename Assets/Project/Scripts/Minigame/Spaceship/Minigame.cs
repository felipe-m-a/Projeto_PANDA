using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Spaceship
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Obstacle obstaclePrefab;
        [SerializeField] private Spaceship spaceship;
        [SerializeField] private float spawnLocationMinY = -3.5f;
        [SerializeField] private float spawnLocationMaxY = 2f;

        private float _spawnDelay = 2f;
        private float _spawnLocationX;
        
        private bool isGameOver;
        
        public event Action SolvedEvent;
        
        private void Start()
        {
            _spawnLocationX = Camera.main.ScreenToWorldPoint(Vector3.right * Screen.width).x + 3f;
            
            Restart();
        }

        private void OnEnable()
        {
            Invoke(nameof(Spawn), _spawnDelay);
        }


        private void Spawn()
        {
            if (isGameOver)  return;
            
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.Initialize(new Vector3(_spawnLocationX, Random.Range(spawnLocationMinY, spawnLocationMaxY)));
            
            Invoke(nameof(Spawn), _spawnDelay);
        }

        private void Restart()
        {
            DestroyAllObstacles();
            spaceship.transform.position = Vector3.zero;
        }

        public void DestroyAllObstacles() 
        {
            foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle")) 
            {
                Destroy(obstacle);
            }
        }
    }
}