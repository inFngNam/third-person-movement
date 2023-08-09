using Character;
using Character.Stats;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ProtagonistData", menuName = "Data/ProtagonistData")]
    public class ProtagonistStats : ScriptableObject
    {
        [field: SerializeField] public CharacterStats BaseStats { get; private set; }
    }
}