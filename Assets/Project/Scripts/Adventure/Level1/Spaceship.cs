using Project.Scripts.Adventure.InteractionSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Spaceship : MonoBehaviour, IInteractable
    {
        [SerializeField] private Renderer interactableFocusIcon;
        [SerializeField] private StoryTracker storyTracker;

        public bool CanInteract()
        {
            return true;
        }

        public void GainFocus()
        {
            interactableFocusIcon.enabled = true;
        }

        public void LoseFocus()
        {
            interactableFocusIcon.enabled = false;
        }

        public void Interact()
        {
            var dialogue = new Dialogue(transform);

            if (!storyTracker.receivedParts)
            {
                dialogue.Add("Sua nave está quebrada! Você precisa de peças para consertá-la.");
            }
            else if (!storyTracker.fixedSpaceship)
            {
                dialogue.Add("Peças necessárias encontradas.\nIniciando conserto da nave.");
                storyTracker.deliveredParts = true;
            }
            else
            {
                dialogue.Add("Nave consertada. Preparando para decolagem.");
            }

            EventBus.TriggerDialogue(dialogue);
        }
    }
}