using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Whack
{
    public class Minigame : MonoBehaviour
    {
        private const int Rows = 3;
        private const int Columns = 6;
        [SerializeField] private Settings settings;
        [SerializeField] private UIController uiController;
        [SerializeField] private Board board;
        [SerializeField] private Tile tilePrefab;

        private FlatGrid<Tile> _tiles;
        private int _missingPoints;
        private float _spawnDelay;
        private HashSet<int> _activeTiles;

        public event Action SolvedEvent;

        private void Start()
        {
            _missingPoints = settings.CurrentDifficultySettings.minigameWhackPoints;
            _spawnDelay = settings.CurrentDifficultySettings.minigameWhackSpawnDelay;

            _tiles = new FlatGrid<Tile>(Rows, Columns);
            board.SetDimensions(Rows, Columns);

            _activeTiles = new HashSet<int>();

            SpawnTiles();
        }

        private float elapsed;

        private void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed < _spawnDelay)
                return;

            if (_activeTiles.Count > Math.Min(_tiles.Count / 5, _missingPoints)) return;
            var index = Random.Range(0, _tiles.Count);
            if (_activeTiles.Contains(index))
                return;
            _tiles[index].Activate();
            _activeTiles.Add(index);
            elapsed = 0f;
        }

        private void SpawnTiles()
        {
            for (var i = 0; i < _tiles.Count; i++)
            {
                var tile = Instantiate(tilePrefab, board.transform);
                tile.Initialize(i);
                tile.HideEvent += OnHide;
                _tiles[i] = tile;
            }
        }

        private void OnHide(Tile tile, bool wasHit)
        {
            _activeTiles.Remove(tile.Index);
            if (!wasHit) return;
            _missingPoints -= 1;
            StartCoroutine(CheckIfSolvedRoutine());
        }

        private IEnumerator CheckIfSolvedRoutine()
        {
            if (_missingPoints > 0)
                yield break;

            board.SetInteractable(false);
            yield return new WaitForSeconds(0.5f);
            SolvedEvent?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var tile in _tiles) tile.HideEvent -= OnHide;
        }
    }
}