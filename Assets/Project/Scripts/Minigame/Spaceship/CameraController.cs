using UnityEngine;

namespace Project.Scripts.Minigame.Spaceship
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Background background;
        
        private void Awake()
        {
            Camera mainCamera = Camera.main!;
            const float targetRatio = 16f / 9f;
            float screenRatio = Screen.width / (float)Screen.height;

            if (screenRatio > targetRatio)
            {
                mainCamera.orthographicSize = 9f / 2f;
            }
            else
            {
                mainCamera.orthographicSize = 9f / 2f * targetRatio / screenRatio;
            }

            float height = mainCamera.orthographicSize * 2f;
            float width = mainCamera.aspect * height;
            
            background.transform.localScale = new Vector3(width, height, 1f);
        }
    }
}