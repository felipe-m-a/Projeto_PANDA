using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame.Memory
{
    public static class Generator
    {
        public static FlatGrid<int> Generate(int rows, int columns)
        {
            var grid = new FlatGrid<int>(rows, columns);

            var symbols = new List<int>();
            for (var symbol = 0; symbol < grid.Count / 2; symbol++)
            {
                symbols.Add(symbol);
                symbols.Add(symbol);
            }

            symbols = symbols.OrderBy(_ => Random.value).ToList();

            for (var i = 0; i < symbols.Count; i++) grid[i] = symbols[i];

            return grid;
        }
    }
}