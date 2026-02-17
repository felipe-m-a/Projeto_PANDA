using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.SceneManagementSystem
{
    public class SceneController : MonoBehaviour
    {
        private readonly Dictionary<GameScene.SceneType, string> _loadedScenes = new();
        private bool _isBusy;

        public bool HasLoaded(GameScene.SceneType sceneType)
        {
            return _loadedScenes.ContainsKey(sceneType);
        }

        public Coroutine ExecutePlan(SceneTransitionPlan plan)
        {
            if (_isBusy)
            {
                Debug.LogWarning("Uma transição de cena já está em curso");
                return null;
            }

            _isBusy = true;
            return StartCoroutine(ExecutePlanRoutine(plan));
        }

        private IEnumerator ExecutePlanRoutine(SceneTransitionPlan plan)
        {
            foreach (var sceneType in plan.SceneTypesToUnload)
                if (_loadedScenes.Remove(sceneType, out var scenePath))
                    yield return SceneManager.UnloadSceneAsync(scenePath);

            foreach (var (type, path) in plan.ScenesToLoad)
            {
                _loadedScenes.Add(type, path);
                yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);

                if (path != plan.SceneToActivate) continue;
                var scene = SceneManager.GetSceneByPath(path);
                if (scene.IsValid() && scene.isLoaded)
                    SceneManager.SetActiveScene(scene);
            }

            _isBusy = false;
        }

        #region Singleton

        public static SceneController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        #endregion
    }
}