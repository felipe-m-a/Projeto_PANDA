using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Flow
{
    [RequireComponent(typeof(AspectRatioFitter))]
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Board : MonoBehaviour, IPointerExitHandler
    {
        private AspectRatioFitter _aspectRatioFitter;
        private CanvasGroup _canvasGroup;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _aspectRatioFitter = GetComponent<AspectRatioFitter>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke();
        }

        public event Action PointerExitEvent;

        public void SetDimensions(int rows, int columns)
        {
            _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            _aspectRatioFitter.aspectRatio = columns / (float)rows;

            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = columns;

            var size = _rectTransform.rect.height / rows;
            _gridLayoutGroup.cellSize = new Vector2(size, size);
        }

        public void SetNonInteractable()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}