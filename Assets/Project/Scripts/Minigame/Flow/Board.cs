using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Flow
{
    [RequireComponent(typeof(AspectRatioFitter))]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class Board : MonoBehaviour, IPointerUpHandler, IPointerExitHandler
    {
        private AspectRatioFitter _aspectRatioFitter;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _aspectRatioFitter = GetComponent<AspectRatioFitter>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke();
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent?.Invoke();
        }

        public event Action PointerExitEvent;
        public event Action PointerUpEvent;

        public void SetDimensions(int rows, int columns)
        {
            _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            _aspectRatioFitter.aspectRatio = columns / (float)rows;

            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = columns;

            var size = _rectTransform.rect.height / rows;
            _gridLayoutGroup.cellSize = new Vector2(size, size);
        }
    }
}