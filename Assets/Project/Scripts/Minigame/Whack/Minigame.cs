using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Minigame.Whack
{
    public class Minigame : MonoBehaviour
    {
        private const int Rows = 4;
        private const int Columns = 7;
        [SerializeField] private Settings settings;
        [SerializeField] private Board board;
        [SerializeField] private Tile tilePrefab;

        private int _missingPoints;
        private float _reactionTime;
        private float _spawnDelay;

        private FlatGrid<Tile> _tiles;

        private void Start()
        {
            _missingPoints = settings.CurrentDifficultySettings.minigameWhackPoints;
            _reactionTime = settings.CurrentDifficultySettings.minigameWhackReactionTime;
            _spawnDelay = settings.CurrentDifficultySettings.minigameWhackSpawnDelay;

            _tiles = new FlatGrid<Tile>(Rows, Columns);
            board.SetDimensions(Rows, Columns);

            SpawnTiles();
        }

        private void OnDisable()
        {
            foreach (var tile in _tiles) tile.HideEvent -= OnTileClick;
        }

        public event Action SolvedEvent;


        private void SpawnTiles()
        {
            for (var i = 0; i < _tiles.Count; i++)
            {
                var tile = Instantiate(tilePrefab, board.transform);
                tile.HideEvent += OnTileClick;
                _tiles[i] = tile;
            }
        }

        private void OnTileClick(bool wasHit)
        {
        }

        private IEnumerator CheckIfSolvedRoutine()
        {
            if (_missingPoints > 0)
                yield break;

            board.SetInteractable(false);
            yield return new WaitForSeconds(1f);
            SolvedEvent?.Invoke();
        }
    }
}