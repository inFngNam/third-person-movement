using Data;
using UnityEngine;

namespace Manager
{
    public class DataManager : MonoBehaviour
    {
        [field: SerializeField] public ProtagonistData ProtagonistStats { get; private set; }
    }
}