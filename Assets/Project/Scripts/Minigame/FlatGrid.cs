using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame
{
    public class FlatGrid<T> : IReadOnlyList<T>
    {
        private readonly T[] _data;
        public readonly int Columns;
        public readonly int Rows;

        public FlatGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _data = new T[rows * columns];
        }

        public T this[int row, int column]
        {
            get => _data[GetFlatIndex(row, column)];
            set => _data[GetFlatIndex(row, column)] = value;
        }

        public int Count => _data.Length;

        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<int> NeighborIndices(int index)
        {
            var row = RowOf(index);
            var column = ColumnOf(index);
            if (row > 0)
                yield return index - Columns; // Up
            if (row < Rows - 1)
                yield return index + Columns; // Down
            if (column > 0)
                yield return index - 1; // Left
            if (column < Columns - 1)
                yield return index + 1; // Right
        }

        public IEnumerable<T> NeighborValues(int index)
        {
            return NeighborIndices(index).Select(neighborIndex => _data[neighborIndex]);
        }

        public IEnumerable<Direction> NeighborDirections(int index)
        {
            var row = RowOf(index);
            var column = ColumnOf(index);
            if (row > 0)
                yield return Direction.Up;
            if (row < Rows - 1)
                yield return Direction.Down;
            if (column > 0)
                yield return Direction.Left;
            if (column < Columns - 1)
                yield return Direction.Right;
        }

        public bool TryGetNeighborIndexInDirection(int index, Direction direction, out int neighborIndex)
        {
            switch (direction)
            {
                case Direction.Up:
                    neighborIndex = index - Columns;
                    return RowOf(index) > 0;
                case Direction.Down:
                    neighborIndex = index + Columns;
                    return RowOf(index) < Rows - 1;
                case Direction.Left:
                    neighborIndex = index - 1;
                    return ColumnOf(index) > 0;
                case Direction.Right:
                    neighborIndex = index + 1;
                    return ColumnOf(index) < Columns - 1;
                default:
                    neighborIndex = -1;
                    return false;
            }
        }

        public bool TryGetDirectionBetween(int indexA, int indexB, out Direction direction)
        {
            foreach (var d in NeighborDirections(indexA))
                if (TryGetNeighborIndexInDirection(indexA, d, out var nIndex) && nIndex == indexB)
                {
                    direction = d;
                    return true;
                }

            direction = default;
            return false;
        }

        public bool AreNeighbors(int indexA, int indexB)
        {
            return Mathf.Abs(RowOf(indexA) - RowOf(indexB)) + Mathf.Abs(ColumnOf(indexA) - ColumnOf(indexB)) == 1;
        }

        public int RowOf(int index)
        {
            return index / Columns;
        }

        public int ColumnOf(int index)
        {
            return index % Columns;
        }

        public int GetFlatIndex(int row, int column)
        {
            return row * Columns + column;
        }
    }
}