using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Puzzle
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Board board;

        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Sprite[] sprites;
        private int _blankTileIndex;

        private FlatGrid<Tile> _tiles;

        private void Start()
        {
            var size = settings.CurrentDifficultySettings.minigamePuzzleSize;
            var expectedMoveCount = settings.CurrentDifficultySettings.minigamePuzzleExpectedMoveCount;

            _tiles = new FlatGrid<Tile>(size, size);
            board.SetDimensions(size, size);

            var (tileIds, blankId) = Generator.Generate(size, expectedMoveCount);

            SpawnTiles(tileIds, blankId);
        }

        private void OnDisable()
        {
            foreach (var tile in _tiles) tile.PointerClickEvent -= OnTileClick;
        }

        public event Action SolvedEvent;


        private void SpawnTiles(FlatGrid<int> tileIds, int blankId)
        {
            var cutSprites = SplitIntoSprite(sprites[Random.Range(0, sprites.Length)], tileIds.Rows, tileIds.Columns);
            for (var i = 0; i < tileIds.Count; i++)
            {
                var tile = Instantiate(tilePrefab, board.transform);
                var id = tileIds[i];
                if (id == blankId)
                {
                    tile.Initialize(i, id, null);
                    _blankTileIndex = i;
                }
                else
                {
                    tile.Initialize(i, id, cutSprites[id]);
                }

                tile.PointerClickEvent += OnTileClick;
                _tiles[i] = tile;
            }
        }

        private void OnTileClick(Tile tile)
        {
            if (tile.Index == _blankTileIndex)
                return;
            if (!_tiles.AreNeighbors(tile.Index, _blankTileIndex))
                return;

            StartCoroutine(SlideRoutine(tile));
        }

        private IEnumerator SlideRoutine(Tile tile)
        {
            board.SetInteractable(false);
            var blankTile = _tiles[_blankTileIndex];
            yield return tile.SlideToRoutine(blankTile.transform.position);
            tile.Swap(blankTile);
            board.ForceLayoutRebuild();
            board.SetInteractable(true);

            _blankTileIndex = tile.Index;
            yield return CheckIfSolvedRoutine();
        }

        private IEnumerator CheckIfSolvedRoutine()
        {
            for (var i = 0; i < _tiles.Count; i++)
                if (_tiles[i].TileId != i)
                    yield break;

            board.SetInteractable(false);
            yield return new WaitForSeconds(1f);
            SolvedEvent?.Invoke();
        }


        private static List<Sprite> SplitIntoSprite(Sprite fullSprite, int rows, int columns)
        {
            var width = fullSprite.texture.width / rows;
            var height = fullSprite.texture.height / columns;

            var sprites = new List<Sprite>(rows * columns);

            for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
            {
                var rect = new Rect(column * width, (rows - row - 1) * height, width, height);
                var sprite = Sprite.Create(fullSprite.texture, rect, Vector2.one / 2f, fullSprite.pixelsPerUnit);
                sprites.Add(sprite);
            }

            return sprites;
        }
    }
}