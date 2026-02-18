using Project.Scripts.Adventure.InteractionSystem;
using UnityEngine;

namespace Project.Scripts.Adventure.Level1
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private Renderer interactableFocusIcon;
        [SerializeField] private DialogueManager dialogueManager;
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
            if (!storyTracker.receivedQuest)
            {
                dialogue.AddLine("Olá! Minha nave está quebrada e preciso de peças para consertá-la.", "Jogador");
                dialogue.AddLine("Ah, eu posso ajudar com isso! Mas antes, você poderia fazer algo por mim?", "Oobi");
                dialogue.AddLine("O que você precisa?", "Jogador");
                dialogue.AddLine("Eu preciso de 3 moedas. Você poderia coletá-las para mim?", "Oobi");
                dialogue.AddLine("Certo! Vou buscar as moedas. Onde eu posso encontrá-las?", "Jogador");
                dialogue.AddLine(
                    "Você pode procurar na floresta ao norte. Existem alguns lugares onde as moedas costumam aparecer.",
                    "Oobi");
                dialogue.AddLine("Assim que coletá-las, volte aqui!", "Oobi");
                dialogue.AddLine("Pode deixar! Vou lá agora mesmo.", "Jogador");
                dialogue.AddLine("Estou contando com você! Boa sorte!", "Oobi");
                dialogueManager.StartDialogue(dialogue);

                storyTracker.receivedQuest = true;
            }
            else if (storyTracker.collectedCoinsCount < 3)
            {
                dialogue.AddLine("Ainda faltam moedas. Volte quando tiver todas!", "Oobi");
                dialogueManager.StartDialogue(dialogue);
            }
            else if (!storyTracker.receivedParts)
            {
                dialogue.AddLine("Oi! Aqui estão as duas moedas que você pediu!", "Jogador");
                dialogue.AddLine("Excelente! Agora só preciso que faça mais uma coisa.", "Oobi");
                dialogue.AddLine("Perfeito! O que devo fazer agora?", "Jogador");
                dialogue.AddLine("Resolva este desafio!", "Oobi");
                // dialogueManager.StartDialogue(dialogue, GameScene.MinigameMemory);
                storyTracker.receivedParts = true;
            }
            else
            {
                dialogue.AddLine("Se precisar de mais alguma coisa, pode me avisar. Estou sempre aqui para ajudar!",
                    "Oobi");
                dialogue.AddLine("Vou lembrar disso! Até mais!", "Jogador");
                dialogueManager.StartDialogue(dialogue);
            }
        }
    }
}