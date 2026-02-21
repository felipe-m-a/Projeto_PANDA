using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Minigame.Pipes
{
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float turnDuration = 0.3f;
        [SerializeField] private Image pipe;
        [SerializeField] private Image water;

        public bool isInteractable = true;
        public Direction[] connections;

        public bool IsConnected
        {
            get => water.enabled;
            set => water.enabled = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                StartCoroutine(OnClickRoutine());
        }

        public event Action<Tile> PointerClickEvent;

        public void Initialize(HashSet<Direction> directions, int extraTurns)
        {
            while (!directions.SetEquals(connections)) Turn(true);

            for (var i = 0; i < extraTurns; i++) Turn(true);
        }

        private void Turn(bool rotateImage)
        {
            for (var i = 0; i < connections.Length; i++)
                connections[i] = connections[i].Turn();
            if (rotateImage)
                pipe.transform.eulerAngles += new Vector3(0, 0, -90f);
        }

        private static IEnumerator RotateBy(Transform transform, Quaternion delta, float duration)
        {
            var start = transform.rotation;
            var end = start * delta;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                transform.rotation = Quaternion.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = end;
        }

        private IEnumerator OnClickRoutine()
        {
            isInteractable = false;
            Turn(false);
            yield return RotateBy(pipe.transform, Quaternion.AngleAxis(-90f, Vector3.forward), turnDuration);
            isInteractable = true;
            PointerClickEvent?.Invoke(this);
        }
    }
}