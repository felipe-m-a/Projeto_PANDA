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
            foreach (var dSettings in new[] { easyDifficultySettings, mediumDifficultySettings, hardDifficultySettings })
            {
                Debug.Assert(dSettings.minigameFlowColumns >= dSettings.minigameFlowRows, "O minigame flow não pode ter menos linhas que colunas");
                Debug.Assert(dSettings.minigameFlowColumns <= minigameFlowColors.Count, "O minigame flow não cores suficientes");
            }
        }

        [Serializable]
        public class DifficultySettings
        {
            [Header("Minigame Flow")] [Min(5)] public int minigameFlowRows;
            [Min(5)] public int minigameFlowColumns;
        }
    }
}