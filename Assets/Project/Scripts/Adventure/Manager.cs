using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private UIController uiController;

        private void Start()
        {
            inputReader.EnablePlayerInput();
        }

        private void OnDisable()
        {
            inputReader.DisableAllInput();
        }
    }
}