using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Memory
{
    [RequireComponent(typeof(AspectRatioFitter))]
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Board : MonoBehaviour
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

        public void SetDimensions(int rows, int columns)
        {
            _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            _aspectRatioFitter.aspectRatio = columns / (float)rows;

            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = columns;

            var size = _rectTransform.rect.height / rows;
            _gridLayoutGroup.cellSize = new Vector2(size, size);
        }

        public void SetInteractable(bool interactable)
        {
            _canvasGroup.interactable = interactable;
            _canvasGroup.blocksRaycasts = interactable;
        }
    }
}