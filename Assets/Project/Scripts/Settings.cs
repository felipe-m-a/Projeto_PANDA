using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
    public class Settings : ScriptableObject
    {
        public int difficultyIndex;
        public bool showOnScreenControls;
        public List<Color> minigameFlowColors;

        [Header("Difficulty Settings")] [SerializeField]
        private DifficultySettings easyDifficultySettings;

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
                Debug.Assert(d.minigameFlowColumns <= minigameFlowColors.Count, "Minigame Flow: Faltam cores");

                Debug.Assert(d.minigameMemoryRows * d.minigameMemoryColumns % 2 == 0, "Minigame Memory: A quantidade de cartas tem que ser par");
            }
        }

        [Serializable]
        public class DifficultySettings
        {
            [Header("Minigame Flow")] [Min(5)] public int minigameFlowRows;
            [Min(5)] public int minigameFlowColumns;

            [Header("Minigame Memory")] [Min(3)] public int minigameMemoryRows;
            [Min(4)] public int minigameMemoryColumns;
        }
    }
}