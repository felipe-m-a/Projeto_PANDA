using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class OnScreenControlsController : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}