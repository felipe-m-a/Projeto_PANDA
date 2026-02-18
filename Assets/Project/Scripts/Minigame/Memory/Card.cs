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

        private Color _color;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                PointerClickEvent?.Invoke(this);
        }

        public event Action<Card> PointerClickEvent;

        public void Initialize(Color color, Sprite sprite)
        {
            _color = color;
            symbol.sprite = sprite;
        }

        public bool Matches(Card other)
        {
            return symbol.sprite == other.symbol.sprite && _color == other._color;
        }

        public IEnumerator Show()
        {
            yield return RotateTo(transform, Quaternion.Euler(0f, 90f, 0f), flipDuration / 2f);
            symbol.color = _color;
            yield return RotateTo(transform, Quaternion.Euler(0f, 180f, 0f), flipDuration / 2f);
        }

        public IEnumerator Hide()
        {
            yield return RotateTo(transform, Quaternion.Euler(0f, 90f, 0f), flipDuration / 2f);
            symbol.color = Color.clear;
            yield return RotateTo(transform, Quaternion.Euler(0f, 0f, 0f), flipDuration / 2f);
        }

        private static IEnumerator RotateTo(Transform transform, Quaternion end, float duration)
        {
            var start = transform.rotation;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                transform.rotation = Quaternion.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = end;
        }
    }
}