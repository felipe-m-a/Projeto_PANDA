using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Pipes
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Board board;

        [SerializeField] private Tile fullTilePrefab;
        [SerializeField] private Tile tripleTilePrefab;
        [SerializeField] private Tile straightTilePrefab;
        [SerializeField] private Tile curvedTilePrefab;
        [SerializeField] private Tile endTilePrefab;
        private int _startTileIndex;

        private FlatGrid<Tile> _tiles;

        private void Start()
        {
            var rows = settings.CurrentDifficultySettings.minigamePipesRows;
            var columns = settings.CurrentDifficultySettings.minigamePipesColumns;

            _tiles = new FlatGrid<Tile>(rows, columns);
            board.SetDimensions(rows, columns);

            var tileDirections = Generator.Generate(rows, columns);

            SpawnTiles(tileDirections);
            PickStartTile();
            MarkConnected();
        }

        private void OnDisable()
        {
            foreach (var tile in _tiles) tile.PointerClickEvent -= OnTileClick;
        }

        public event Action SolvedEvent;

        private void SpawnTiles(FlatGrid<HashSet<Direction>> tileDirections)
        {
            for (var i = 0; i < tileDirections.Count; i++)
            {
                var tile = Instantiate(SelectPrefab(tileDirections[i]), board.transform);
                tile.Initialize(tileDirections[i], ComputeExtraTurns(tileDirections[i]));
                tile.PointerClickEvent += OnTileClick;
                _tiles[i] = tile;
            }
        }

        private Tile SelectPrefab(HashSet<Direction> directions)
        {
            if (directions.Count == 0) return null;

            return directions.Count switch
            {
                1 => endTilePrefab,
                2 => directions.Contains(directions.First().Opposite()) ? straightTilePrefab : curvedTilePrefab,
                3 => tripleTilePrefab,
                _ => fullTilePrefab
            };
        }

        private static int ComputeExtraTurns(HashSet<Direction> directions)
        {
            if (directions.Count == 0) return 0;

            return directions.Count switch
            {
                1 => Random.Range(1, 3),
                2 => Random.Range(1, directions.Contains(directions.First().Opposite()) ? 2 : 3),
                3 => Random.Range(1, 3),
                _ => 0
            };
        }

        private void OnTileClick(Tile tile)
        {
            MarkConnected();
        }

        private void MarkConnected()
        {
            foreach (var t in _tiles) t.IsConnected = false;

            var queue = new Queue<int>();
            queue.Enqueue(_startTileIndex);

            while (queue.Count > 0)
            {
                var index = queue.Dequeue();
                _tiles[index].IsConnected = true;

                print($"{index} - {string.Join(", ", _tiles[index].connections)}");

                foreach (var direction in _tiles[index].connections)
                    if (_tiles.TryGetNeighborIndexInDirection(index, direction, out var neighborIndex)
                        && _tiles[neighborIndex].connections.Contains(direction.Opposite())
                        && !_tiles[neighborIndex].IsConnected)
                        queue.Enqueue(neighborIndex);
            }

            if (_tiles.All(t => t.IsConnected)) SolvedEvent?.Invoke();
        }

        private void PickStartTile()
        {
            var ends = new List<int>();
            for (var i = 0; i < _tiles.Count; i++)
                if (_tiles[i].connections.Length == 1)
                    ends.Add(i);

            var rIndex = Random.Range(0, ends.Count);
            _startTileIndex = ends[rIndex];
        }
    }
}