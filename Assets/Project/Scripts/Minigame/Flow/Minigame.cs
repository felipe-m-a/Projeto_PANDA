using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame.Flow
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Board board;

        private Color? _currentColor;

        private Dictionary<int, Color> _ends = new();
        private FlatGrid<Tile> _grid;


        private void Start()
        {
            var rows = settings.CurrentDifficultySettings.minigameFlowRows;
            var columns = settings.CurrentDifficultySettings.minigameFlowColumns;

            board.SetDimensions(rows, columns);
            board.PointerUpEvent += OnCancelLinking;
            board.PointerExitEvent += OnCancelLinking;

            var ends = Generator.Generate(rows, columns);
            _ends = ends.ToDictionary(pair => pair.Key, pair => settings.minigameFlowColors[pair.Value]);
            _grid = new FlatGrid<Tile>(rows, columns);

            SpawnTiles(rows, columns);
        }


        private void OnDisable()
        {
            board.PointerUpEvent -= OnCancelLinking;
            board.PointerExitEvent -= OnCancelLinking;

            foreach (var tile in _grid)
            {
                tile.PointerDownEvent -= OnStartLinking;
                tile.PointerEnterEvent -= OnLinking;
            }
        }

        private void SpawnTiles(int rows, int columns)
        {
            for (var i = 0; i < rows * columns; i++)
            {
                var tile = Instantiate(tilePrefab, board.transform);

                if (_ends.TryGetValue(i, out var color))
                {
                    tile.InitializeEnd(i, color);
                    tile.PointerDownEvent += OnStartLinking;
                    tile.PointerEnterEvent += OnLinking;
                }
                else
                {
                    tile.InitializeNormal(i);
                    tile.PointerEnterEvent += OnLinking;
                }

                _grid[i] = tile;
            }
        }

        private void OnStartLinking(Tile tile)
        {
            _currentColor = _ends[tile.Index];

            Unlink(_currentColor.Value);

            tile.LinkColor = _currentColor;
        }

        private void OnLinking(Tile tile)
        {
            // Tem que estar marcando
            if (!_currentColor.HasValue)
                return;

            // Não pode já ser dessa cor e tem que ter exatamente 1 vizinho dessa cor
            if (tile.LinkColor == _currentColor.Value
                || _grid.NeighborValues(tile.Index).Count(t => t.LinkColor == _currentColor) != 1)
            {
                Unlink(_currentColor.Value);
                _currentColor = null;
                return;
            }

            // Se uma ponta
            if (_ends.TryGetValue(tile.Index, out var endColor))
            {
                // Se for a outra ponta dessa cor
                if (endColor == _currentColor.Value)
                    Link(tile);
                else
                    Unlink(_currentColor.Value);
                _currentColor = null;
                return;
            }

            // Se ja tiver marcado com outra cor
            if (tile.LinkColor.HasValue)
                Unlink(tile.LinkColor.Value);

            Link(tile);
        }

        private void OnCancelLinking()
        {
            // Tem que estar marcando
            if (!_currentColor.HasValue)
                return;

            Unlink(_currentColor.Value);
        }

        private void Unlink(Color color)
        {
            foreach (var tile in _grid.Where(t => t.LinkColor == color).ToList()) tile.Unlink();
        }

        private void Link(Tile tile)
        {
            tile.LinkColor = _currentColor;

            var previousTile = _grid.NeighborValues(tile.Index)
                .First(t => t.LinkColor == _currentColor);

            _grid.TryGetDirectionBetween(tile.Index, previousTile.Index, out var direction);
            tile.Link(direction);
            previousTile.Link(direction.Opposite());
        }
    }
}