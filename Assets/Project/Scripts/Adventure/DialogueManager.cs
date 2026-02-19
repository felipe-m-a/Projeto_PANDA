using Project.Scripts.Adventure.InteractionSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachine;
        [SerializeField] private UIController uiController;
        [SerializeField] private InputReader inputReader;

        private Dialogue _dialogue;
        private int _lineIndex;

        private void OnEnable()
        {
            EventBus.DialogueTriggered += StartDialogue;
            inputReader.AdvanceDialogueEvent += HandleInput;
            uiController.DialogueClicked += AdvanceDialogue;
        }

        private void OnDisable()
        {
            EventBus.DialogueTriggered -= StartDialogue;
            inputReader.AdvanceDialogueEvent -= HandleInput;
            uiController.DialogueClicked -= AdvanceDialogue;
        }

        private void StartDialogue(Dialogue dialogue)
        {
            inputReader.EnableDialogueInput();
            _dialogue = dialogue;
            _lineIndex = 0;

            EventBus.RaiseDialogueStarted();
            cinemachine.Target.TrackingTarget = _dialogue.Target;
            cinemachine.Priority = 10;

            AdvanceDialogue();
            uiController.ShowDialogue();
        }


        private void EndDialogue()
        {
            cinemachine.Priority = -10;
            uiController.HideDialogue();

            EventBus.RaiseDialogueEnded();
            inputReader.EnablePlayerInput();
        }

        private void AdvanceDialogue()
        {
            print($"{_dialogue.Lines.Count} > {_lineIndex}");
            if (_dialogue.Lines.Count > _lineIndex)
                uiController.UpdateDialogueText(_dialogue.Lines[_lineIndex++]);
            else
                EndDialogue();
        }

        private void HandleInput()
        {
            print("Input");
            if (_dialogue != null)
                AdvanceDialogue();
        }
    }
}