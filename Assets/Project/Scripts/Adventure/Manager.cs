using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private UIController uiController;

        private void Start()
        {
            inputReader.EnableAllInput();
        }

        private void OnDisable()
        {
            inputReader.DisableAllInput();
        }
    }
}