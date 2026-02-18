using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Memory
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image symbol;
        [SerializeField] private float flipDuration = 0.3f;

        public bool isInteractable = true;
        public bool isMatched;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                PointerClickEvent?.Invoke(this);
        }

        public event Action<Card> PointerClickEvent;

        public void Initialize(Sprite sprite)
        {
            symbol.sprite = sprite;
        }

        public bool Matches(Card other)
        {
            return symbol.sprite == other.symbol.sprite;
        }

        public IEnumerator Show()
        {
            yield return Animations.RotateTo(transform, Quaternion.Euler(0f, 90f, 0f), flipDuration / 2f);
            symbol.color = Color.white;
            yield return Animations.RotateTo(transform, Quaternion.Euler(0f, 180f, 0f), flipDuration / 2f);
        }

        public IEnumerator Hide()
        {
            yield return Animations.RotateTo(transform, Quaternion.Euler(0f, 90f, 0f), flipDuration / 2f);
            symbol.color = Color.clear;
            yield return Animations.RotateTo(transform, Quaternion.Euler(0f, 0f, 0f), flipDuration / 2f);
        }
    }
}