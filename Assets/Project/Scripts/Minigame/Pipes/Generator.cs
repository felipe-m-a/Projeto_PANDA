using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame.Pipes
{
    public static class Generator
    {
        public static FlatGrid<HashSet<Direction>> Generate(int rows, int columns)
        {
            var grid = new FlatGrid<HashSet<Direction>>(rows, columns);
            for (var i = 0; i < grid.Count; i++) grid[i] = new HashSet<Direction>();

            var directions = new HashSet<Direction> { DirectionExtensions.Directions[Random.Range(0, DirectionExtensions.Directions.Count)] };
            directions.Add(directions.First().Turn());

            for (var index = 0; index < grid.Count; index++)
            {
                var options = grid.NeighborDirections(index)
                    .Intersect(directions)
                    .ToList();

                if (options.Count == 0)
                    continue;

                var chosen = options[Random.Range(0, options.Count)];

                grid[index].Add(chosen);

                grid.TryGetNeighborIndexInDirection(index, chosen, out var neighbour);
                grid[neighbour].Add(chosen.Opposite());
            }

            return grid;
        }
    }
}