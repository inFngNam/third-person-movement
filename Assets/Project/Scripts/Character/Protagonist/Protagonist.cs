using Character.Stats;
using Data;
using System;
using UnityEngine;

namespace Character
{
    public class Protagonist : MonoBehaviour
    {
        public static Protagonist Instance { get; private set; }
        private ProtagonistStats m_Data;
        [SerializeField] private CharacterStats stats;

        public event Action<LocomotionStats> OnChangeLocomotionStats;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(ProtagonistStats protagonistStats)
        {
            m_Data = protagonistStats;
            stats = m_Data.BaseStats;
            OnChangeLocomotionStats?.Invoke(stats.LocomotionStats);
        }
    }
}