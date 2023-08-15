using Protagonist;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DataManager dataManager;

        private void Start()
        {
            TheProtagonist.Instance.Initialize(dataManager.ProtagonistStats);
        }

        private void Update()
        {

        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}