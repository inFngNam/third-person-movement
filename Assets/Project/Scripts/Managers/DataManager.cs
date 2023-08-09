using Data;
using UnityEngine;

namespace Manager
{
    public class DataManager : MonoBehaviour
    {
        [field: SerializeField] public ProtagonistStats ProtagonistStats { get; private set; }
    }
}