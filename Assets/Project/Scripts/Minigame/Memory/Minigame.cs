using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Minigame.Memory
{
    public class Minigame : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Board board;
        [SerializeField] private Sprite[] symbols;

        private FlatGrid<Card> _cards;
        private Card _selectedCard;

        private void Start()
        {
            var rows = settings.CurrentDifficultySettings.minigameMemoryRows;
            var columns = settings.CurrentDifficultySettings.minigameMemoryColumns;

            board.SetDimensions(rows, columns);
            _cards = new FlatGrid<Card>(rows, columns);

            var symbolIdGrid = Generator.Generate(rows, columns);

            SpawnCards(symbolIdGrid);
        }

        private void OnDisable()
        {
            foreach (var card in _cards) card.PointerClickEvent -= OnCardClick;
        }

        public event Action SolvedEvent;

        private void SpawnCards(FlatGrid<int> symbolIdGrid)
        {
            var selectedSymbols = symbols.OrderBy(_ => Random.value).Take(symbolIdGrid.Count / 2).ToList();

            for (var i = 0; i < symbolIdGrid.Count; i++)
            {
                var card = Instantiate(cardPrefab, board.transform);

                var symbol = selectedSymbols[symbolIdGrid[i]];

                card.Initialize(symbol);

                card.PointerClickEvent += OnCardClick;

                _cards[i] = card;
            }
        }

        private IEnumerator SelectCardRoutine(Card card)
        {
            board.SetInteractable(false);
            card.isInteractable = false;

            yield return card.Show();
            if (_selectedCard is null)
            {
                _selectedCard = card;
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                yield return CheckMatch(card);
            }

            board.SetInteractable(true);
        }

        private IEnumerator CheckMatch(Card card)
        {
            if (_selectedCard.Matches(card))
            {
                card.isMatched = true;
                _selectedCard.isMatched = true;

                if (_cards.All(c => c.isMatched)) SolvedEvent?.Invoke();
            }
            else
            {
                StartCoroutine(card.Hide());
                yield return _selectedCard.Hide();

                card.isInteractable = true;
                _selectedCard.isInteractable = true;
            }

            _selectedCard = null;
        }

        private void OnCardClick(Card card)
        {
            StartCoroutine(SelectCardRoutine(card));
        }
    }
}