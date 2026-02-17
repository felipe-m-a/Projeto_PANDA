using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.SceneManagementSystem
{
    public class EditorInitializationLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Awake()
        {
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
                if (SceneManager.GetSceneAt(i).path == GameScene.Initialization.Path)
                    return;

            SceneManager.LoadSceneAsync(GameScene.Initialization.Path, LoadSceneMode.Additive);
        }
#endif
    }
}