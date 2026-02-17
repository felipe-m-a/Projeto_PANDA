using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Minigame.Flow
{
    public static class Generator
    {
        public static Dictionary<int, int> Generate(int rows, int columns)
        {
            var grid = new FlatGrid<int>(rows, columns);
            var chains = new List<List<int>>();

            SetInitialConfiguration(grid, chains);
            Randomize(50000, 3, grid, chains);

            var ends = new Dictionary<int, int>();
            for (var color = 0; color < chains.Count; color++)
            {
                ends[chains[color][0]] = color;
                ends[chains[color][^1]] = color;
            }

            return ends;
        }

        private static void SetInitialConfiguration(FlatGrid<int> grid, List<List<int>> chains)
        {
            // Horizontais
            for (var row = 0; row < grid.Rows; row++)
            {
                var color = chains.Count;
                chains.Add(new List<int>());
                for (var column = 0; column < grid.Rows; column++)
                {
                    grid[row, column] = color;
                    chains[color].Add(grid.GetFlatIndex(row, column));
                }
            }

            // Verticais
            for (var column = grid.Rows; column < grid.Columns; column++)
            {
                var color = chains.Count;
                chains.Add(new List<int>());
                for (var row = 0; row < grid.Rows; row++)
                {
                    grid[row, column] = color;
                    chains[color].Add(grid.GetFlatIndex(row, column));
                }
            }
        }

        private static void Randomize(int maxIterations, int minRows, FlatGrid<int> grid,
            List<List<int>> chains)
        {
            var ends = new List<int>();
            ends.AddRange(chains.Select(chain => chain[0]));
            ends.AddRange(chains.Select(chain => chain[^1]));

            for (var i = 0;
                 i < maxIterations
                 && chains.Min(chain => chain.Select(grid.RowOf).Distinct().Count()) < minRows;
                 i++)
            {
                // Escolhe uma ponta aleatória
                var indexA = ends[Random.Range(0, ends.Count)];
                var colorA = grid[indexA];

                // Seleciona os vizinhos válidos
                var options = grid.NeighborIndices(indexA)
                    .Where(n => grid[n] != grid[indexA]) // Não é da mesma cor
                    .Where(n => ends.Contains(n)) // É uma ponta
                    .Where(n => chains[grid[n]].Count > 4) // A trilha não é pequena demais
                    .Where(n => grid.NeighborIndices(n).Count(nn => grid[nn] == grid[indexA]) ==
                                1) // Não tem outro vizinho da cor A
                    .ToList();

                if (options.Count == 0) continue;

                // Escolhe o vizinho
                var indexB = options[Random.Range(0, options.Count)];
                var colorB = grid[indexB];

                // Marca a nova ponta da trilha B que é o segundo ou o penúltimo
                ends.Add(chains[colorB].IndexOf(indexB) == 0
                    ? chains[colorB][1]
                    : chains[colorB][^2]);
                chains[colorB].Remove(indexB);

                // Atualiza a cor do tile B
                grid[indexB] = colorA;

                // Marca a nova ponta da trilha A no início ou no final
                chains[colorA].Insert(
                    chains[colorA].IndexOf(indexA) == 0 ? 0 : chains[colorA].Count,
                    indexB);
                ends.Remove(indexA);
            }
        }
    }
}