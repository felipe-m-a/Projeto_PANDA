using UnityEngine;

namespace Project.Scripts.Minigame.Spaceship
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Background background;

        private void Awake()
        {
            var mainCamera = Camera.main!;
            const float targetRatio = 16f / 9f;
            var screenRatio = Screen.width / (float)Screen.height;

            if (screenRatio > targetRatio)
                mainCamera.orthographicSize = 9f / 2f;
            else
                mainCamera.orthographicSize = 9f / 2f * targetRatio / screenRatio;

            var height = mainCamera.orthographicSize * 2f;
            var width = mainCamera.aspect * height;

            background.transform.localScale = new Vector3(width, height, 1f);
        }
    }
}