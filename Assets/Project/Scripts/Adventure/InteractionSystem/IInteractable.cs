namespace Project.Scripts.Adventure.InteractionSystem
{
    public interface IInteractable
    {
        public bool CanInteract();

        public void GainFocus();

        public void LoseFocus();

        public void Interact();
    }
}