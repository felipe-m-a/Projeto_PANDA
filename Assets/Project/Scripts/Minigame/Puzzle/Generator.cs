using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame.Puzzle
{
    public static class Generator
    {
        public static (FlatGrid<int> Ids, int BlankIndex) Generate(int size, int expectedMoveCount)
        {
            var grid = new FlatGrid<int>(size, size);
            for (var i = 0; i < grid.Count; i++) grid[i] = i;

            var blankId = grid.Count - 1;
            var blankIndex = blankId;

            Direction? previousMove = null;
            for (var i = 0; i < expectedMoveCount; i++)
            {
                var possibleMoves = grid.NeighborDirections(blankIndex).ToList();

                // Evitar que os movimentos se cancelem
                if (previousMove.HasValue)
                    possibleMoves.Remove(previousMove.Value.Opposite());

                var pickedMove = possibleMoves[Random.Range(0, possibleMoves.Count)];

                grid.TryGetNeighborIndexInDirection(blankIndex, pickedMove, out var targetIndex);

                (grid[blankIndex], grid[targetIndex]) = (grid[targetIndex], grid[blankIndex]);

                blankIndex = targetIndex;
                previousMove = pickedMove;
            }

            return (grid, blankId);
        }
    }
}