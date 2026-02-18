using Project.Scripts.Adventure.InteractionSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachine;

        [SerializeField] private Canvas canvas;

        // [SerializeField] private TMP_Text textMesh;
        [SerializeField] private InputReader inputReader;

        private Dialogue _dialogue;
        private int _lineIndex;

        private string _scheduledMinigame;

        private void OnEnable()
        {
            canvas.enabled = false;
            // inputReader.advanceDialogueEvent += AdvanceDialogue;
        }

        private void OnDisable()
        {
            // inputReader.advanceDialogueEvent += AdvanceDialogue;
        }

        public void StartDialogue(Dialogue dialogue, string minigameScene = null)
        {
            EventBus.RaiseDialogueStarted();
            // inputReader.EnableDialogueInput();
            cinemachine.Target.TrackingTarget = dialogue.Target;
            cinemachine.Priority = 10;
            canvas.enabled = true;

            _dialogue = dialogue;

            _scheduledMinigame = minigameScene;

            AdvanceDialogue();
        }


        private void EndDialogue()
        {
            cinemachine.Priority = -10;
            canvas.enabled = false;
            _lineIndex = 0;
            // inputReader.EnableAdventureInput();

            EventBus.RaiseDialogueEnded();
            if (!string.IsNullOrEmpty(_scheduledMinigame)) EventBus.RaiseMinigameTriggered(_scheduledMinigame);
        }

        public void AdvanceDialogue()
        {
            if (_dialogue.Lines.Count > _lineIndex)
                // textMesh.text = _dialogue.Lines[_lineIndex];
                _lineIndex++;
            else
                EndDialogue();
        }
    }
}