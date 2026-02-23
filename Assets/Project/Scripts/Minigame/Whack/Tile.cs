using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Whack
{
    [RequireComponent(typeof(Image))]
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Sprite hitSprite;

        public bool isInteractable;
        public int Index { get; private set; }

        private Image _image;
        private float _reactionTime;
        private Sprite _defaultSprite;

        public void Initialize(int index)
        {
            Index = index;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            StopAllCoroutines();
            StartCoroutine(HitRoutine());
        }

        public event Action<Tile, bool> HideEvent;

        public void Activate()
        {
            StartCoroutine(ShowAndHideRoutine());
        }


        private IEnumerator ShowAndHideRoutine()
        {
            isInteractable = true;
            _image.color = Color.white;
            yield return new WaitForSeconds(_reactionTime);
            _image.color = Color.clear;
            isInteractable = false;
            HideEvent?.Invoke(this, false);
        }

        private IEnumerator HitRoutine()
        {
            isInteractable = false;
            _image.sprite = hitSprite;
            yield return new WaitForSeconds(_reactionTime / 2f);
            _image.color = Color.clear;
            _image.sprite = _defaultSprite;
            HideEvent?.Invoke(this, true);
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _defaultSprite = _image.sprite;
            
            _reactionTime = settings.CurrentDifficultySettings.minigameWhackReactionTime;
        }
    }
}