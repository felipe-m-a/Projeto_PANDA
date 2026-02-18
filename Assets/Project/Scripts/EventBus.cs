using System;

namespace Project.Scripts
{
    public static class EventBus
    {
        public static event Action DialogueStarted;
        public static event Action DialogueEnded;

        public static event Action MinigameStarted;
        public static event Action MinigameEnded;
        public static event Action<string> MinigameTriggered;

        public static void RaiseDialogueStarted()
        {
            DialogueStarted?.Invoke();
        }

        public static void RaiseDialogueEnded()
        {
            DialogueEnded?.Invoke();
        }

        public static void RaiseMinigameStarted()
        {
            MinigameStarted?.Invoke();
        }

        public static void RaiseMinigameEnded()
        {
            MinigameEnded?.Invoke();
        }

        public static void RaiseMinigameTriggered(string minigameName)
        {
            MinigameTriggered?.Invoke(minigameName);
        }
    }
}