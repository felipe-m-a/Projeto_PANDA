using Project.Scripts.Adventure.InteractionSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private Renderer interactableFocusIcon;
        [SerializeField] private StoryTracker storyTracker;

        private const string NpcName = "Oobi";

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
            if (!storyTracker.receivedQuest)
            {
                dialogue.Add("Jogador", "Olá! Minha nave está quebrada e preciso de peças para consertá-la.")
                    .Add(NpcName, "Ah, eu posso ajudar com isso! Mas antes, você poderia fazer algo por mim?")
                    .Add("Jogador", "O que você precisa?")
                    .Add(NpcName, "Eu preciso de 3 moedas. Você poderia coletá-las para mim?")
                    .Add("Jogador", "Certo! Vou buscar as moedas. Onde eu posso encontrá-las?")
                    .Add(NpcName, "Você pode procurar na floresta ao norte. Existem alguns lugares onde as moedas costumam aparecer.")
                    .Add(NpcName, "Assim que coletá-las, volte aqui!")
                    .Add("Jogador", "Pode deixar! Vou lá agora mesmo.")
                    .Add(NpcName, "Estou contando com você! Boa sorte!");
                storyTracker.receivedQuest = true;
            }
            else if (storyTracker.CollectedCoinsCount < 3 && !storyTracker.deliveredCoins)
            {
                dialogue.Add(NpcName, "Ainda faltam moedas. Volte quando tiver todas!");
            }
            else if (!storyTracker.deliveredCoins)
            {
                dialogue.Add("Jogador", "Oi! Aqui estão as duas moedas que você pediu!");
                dialogue.Add(NpcName, "Excelente! Agora só preciso que faça mais uma coisa.");
                dialogue.Add("Jogador", "Perfeito! O que devo fazer agora?");
                dialogue.Add(NpcName, "Resolva este desafio!");
                storyTracker.deliveredCoins = true;
                storyTracker.SubtractCoins(storyTracker.CollectedCoinsCount);
            }
            else
            {
                dialogue.Add(NpcName, "Se precisar de mais alguma coisa, pode me avisar. Estou sempre aqui para ajudar!");
                dialogue.Add("Jogador", "Vou lembrar disso! Até mais!");
            }

            EventBus.TriggerDialogue(dialogue);
        }
    }
}