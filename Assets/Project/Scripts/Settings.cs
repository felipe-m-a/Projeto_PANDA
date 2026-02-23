using System;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
    public class Settings : ScriptableObject
    {
        public int difficultyIndex;
        public bool showOnScreenControls;
        public Color[] colors;
        public Sprite[] minigameMemorySymbols;

        [SerializeField] private DifficultySettings easyDifficultySettings;
        [SerializeField] private DifficultySettings mediumDifficultySettings;
        [SerializeField] private DifficultySettings hardDifficultySettings;

        public DifficultySettings CurrentDifficultySettings => difficultyIndex switch
        {
            0 => easyDifficultySettings,
            1 => mediumDifficultySettings,
            2 => hardDifficultySettings,
            _ => throw new ArgumentOutOfRangeException()
        };

        private void OnValidate()
        {
            foreach (var d in new[] { easyDifficultySettings, mediumDifficultySettings, hardDifficultySettings })
            {
                Debug.Assert(d.minigameFlowColumns >= d.minigameFlowRows, "Minigame Flow: Não pode ter menos linhas que colunas");
                Debug.Assert(d.minigameFlowColumns <= colors.Length, "Minigame Flow: Faltam cores");

                Debug.Assert(d.minigameMemoryColumns >= d.minigameMemoryRows,
                    "Minigame Memory: Para melhor utilizar o espaço faça colunas >= linhas");
                Debug.Assert(d.minigameMemoryRows * d.minigameMemoryColumns % 2 == 0, "Minigame Memory: A quantidade de cartas tem que ser par");
                Debug.Assert(d.minigameMemoryRows * d.minigameMemoryColumns / 2 <= colors.Length * minigameMemorySymbols.Length,
                    "Minigame Memory: Quantidade de cartas acima do limite");

                Debug.Assert(d.minigamePipesColumns >= d.minigamePipesRows, "Minigame Pipes: Para melhor utilizar o espaço faça colunas >= linhas");

                Debug.Assert(d.minigamePuzzleExpectedMoveCount < d.minigamePuzzleSize * d.minigamePuzzleSize,
                    "Minigame Puzzle: Talvez seja melhor diminuir a quantidade de movimentos");
            }
        }

        [Serializable]
        public class DifficultySettings
        {
            [Header("Minigame Flow")] [Min(5)] public int minigameFlowRows;
            [Min(5)] public int minigameFlowColumns;

            [Header("Minigame Memory")] [Min(3)] public int minigameMemoryRows;
            [Min(4)] public int minigameMemoryColumns;

            [Header("Minigame Pipes")] [Min(4)] public int minigamePipesRows;
            [Min(4)] public int minigamePipesColumns;

            [Header("Minigame Puzzle")] [Min(3)] public int minigamePuzzleSize;
            [Min(4)] public int minigamePuzzleExpectedMoveCount;
        }
    }
}