using Character.Stats;
using Data;
using System;
using UnityEngine;

namespace Protagonist
{
    public class TheProtagonist : MonoBehaviour
    {
        public static TheProtagonist Instance { get; private set; }
        private ProtagonistData m_Data;
        [SerializeField] private CharacterStats stats;
        private ProtagonistLocomotionController locomotionController;

        public event Action<LocomotionStats> OnChangeLocomotionStats;

        private void Awake()
        {
            Instance = this;
            locomotionController = GetComponent<ProtagonistLocomotionController>();
            OnChangeLocomotionStats += locomotionController.OnChangeLocomotionStats;
        }

        private void OnDestroy()
        {
            OnChangeLocomotionStats -= locomotionController.OnChangeLocomotionStats;
        }

        public void Initialize(ProtagonistData protagonistData)
        {
            m_Data = protagonistData;
            stats = m_Data.BaseStats;

            locomotionController.SetControllerConfigures(m_Data.Configure.ControllerConfigure);
            OnChangeLocomotionStats?.Invoke(stats.LocomotionStats);
        }
    }
}