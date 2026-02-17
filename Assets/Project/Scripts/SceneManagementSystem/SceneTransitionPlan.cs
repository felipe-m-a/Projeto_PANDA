using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.SceneManagementSystem
{
    public class SceneTransitionPlan
    {
        private SceneTransitionPlan()
        {
        }

        public Dictionary<GameScene.SceneType, string> ScenesToLoad { get; } = new();
        public HashSet<GameScene.SceneType> SceneTypesToUnload { get; } = new();
        public string SceneToActivate { get; private set; }

        public static SceneTransitionPlan Create()
        {
            return new SceneTransitionPlan();
        }

        public SceneTransitionPlan Load(GameScene scene, bool setActive = false)
        {
            if (scene.Type == GameScene.SceneType.None)
            {
                Debug.LogWarning("Não pode carregar cenas do tipo: " + scene.Path);
            }
            else
            {
                SceneTypesToUnload.Add(scene.Type);
                ScenesToLoad[scene.Type] = scene.Path;
                if (setActive)
                    SceneToActivate = scene.Path;
            }

            return this;
        }

        public SceneTransitionPlan Unload(GameScene.SceneType type)
        {
            if (type == GameScene.SceneType.None)
                Debug.LogWarning("Não pode descarregar cenas do tipo: " + nameof(type));
            else
                SceneTypesToUnload.Add(type);

            return this;
        }

        public Coroutine Perform()
        {
            return SceneController.Instance.ExecutePlan(this);
        }
    }
}