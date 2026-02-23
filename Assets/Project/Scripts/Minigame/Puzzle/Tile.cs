using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Puzzle
{
    [RequireComponent(typeof(Image))]
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float slideDuration = 0.3f;
        public bool isInteractable = true;

        private Image _image;


        public int Index { get; private set; }
        public int TileId { get; private set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                PointerClickEvent?.Invoke(this);
        }

        public event Action<Tile> PointerClickEvent;


        public void Initialize(int index, int id, Sprite sprite)
        {
            Index = index;
            TileId = id;
            SetSprite(sprite);
        }

        private void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.color = sprite ? Color.white : Color.clear;
        }

        public IEnumerator SlideToRoutine(Vector3 target)
        {
            isInteractable = false;
            yield return MoveTo(transform, target, slideDuration);
            isInteractable = true;
        }

        public void Swap(Tile other)
        {
            var sprite = _image.sprite;
            SetSprite(other._image.sprite);
            other.SetSprite(sprite);

            (TileId, other.TileId) = (other.TileId, TileId);
        }

        private static IEnumerator MoveTo(Transform transform, Vector3 end, float duration)
        {
            var start = transform.position;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                transform.position = Vector3.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
        }
    }
}