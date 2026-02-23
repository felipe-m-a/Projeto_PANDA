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
        [SerializeField] private float slideDuration = 0.3f;
        [SerializeField] private Image enemyImage;
        public bool isInteractable = true;


        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                HideEvent?.Invoke(false);
        }

        public event Action<bool> HideEvent;


        public IEnumerator ShowAndHide(Vector3 target)
        {
            isInteractable = false;
            yield return MoveTo(transform, target, slideDuration);
            isInteractable = true;
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