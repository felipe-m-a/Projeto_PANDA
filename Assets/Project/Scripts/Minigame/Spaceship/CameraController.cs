using System;
using UnityEngine;

namespace Project.Scripts.Minigame.Spaceship
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;

        [SerializeField] private Camera mainCamera;

        private Vector2 _cameraSize;
        private float _orthographicSize;


#if UNITY_EDITOR
        private void Update()
#else
        private void Awake()
#endif
        {
            float screenRatio = Screen.width / (float)Screen.height;
            float targetRatio = 16f / 9f;

            if (screenRatio > targetRatio)
            {
                mainCamera.orthographicSize = 9f / 2f;
            }
            else
            {
                mainCamera.orthographicSize = 9f / 2f * targetRatio / screenRatio;
            }

            _orthographicSize = mainCamera.orthographicSize;
            float cameraHeight = _orthographicSize * 2f;
            _cameraSize = new Vector2(mainCamera.aspect * cameraHeight, cameraHeight);
            background.size = _cameraSize;
        }
    }
}