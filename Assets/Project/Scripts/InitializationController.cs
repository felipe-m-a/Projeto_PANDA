using Project.Scripts.SceneManagementSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class InitializationController : MonoBehaviour
    {
        private void Start()
        {
            if (SceneManager.GetActiveScene().path != GameScene.Initialization.Path)
                return;

            SceneTransitionPlan.Create()
                .Load(GameScene.Menu, true)
                .Perform();
        }
    }
}