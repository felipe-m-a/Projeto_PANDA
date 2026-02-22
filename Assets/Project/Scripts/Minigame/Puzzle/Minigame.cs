using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Minigame.Puzzle
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Board board;

        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Tile blankPrefab;
        [SerializeField] private Sprite[] sprites;

        private FlatGrid<Tile> _tiles;

        private void Start()
        {
            var size = settings.CurrentDifficultySettings.minigamePuzzleSize;
            var expectedMoveCount = settings.CurrentDifficultySettings.minigamePuzzleExpectedMoveCount;

            _tiles = new FlatGrid<Tile>(size, size);
            board.SetDimensions(size, size);

            var tileDirections = Generator.Generate(size, expectedMoveCount);

            SpawnTiles(tileDirections);
        }

        private void OnDisable()
        {
            foreach (var tile in _tiles) tile.PointerClickEvent -= OnTileClick;
        }

        public event Action SolvedEvent;


        private void SpawnTiles(FlatGrid<int> tilePositions)
        {
            for (var i = 0; i < tilePositions.Count; i++)
            {
                Tile tile;
                if (tilePositions[i] == tilePositions.Count - 1)
                {
                    tile = Instantiate(blankPrefab, board.transform);
                    tile.Initialize(i, null);
                }
                else
                {
                    tile = Instantiate(tilePrefab, board.transform);
                    tile.Initialize(i, null);
                }

                tile.Initialize(i, null);
                tile.PointerClickEvent += OnTileClick;
                _tiles[i] = tile;
            }
        }

        private void OnTileClick(Tile tile)
        {
        }

        private IEnumerator CheckIfSolvedRoutine()
        {
            for (var i = 0; i < _tiles.Count; i++)
                if (_tiles[i].Index != i)
                    yield break;

            board.SetInteractable(false);
            yield return new WaitForSeconds(1f);
            SolvedEvent?.Invoke();
        }
    }
}