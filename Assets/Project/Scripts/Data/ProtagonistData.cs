using Character;
using Character.Stats;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ProtagonistData", menuName = "Data/ProtagonistData")]
    public class ProtagonistData : ScriptableObject
    {
        [field: SerializeField] public Protagonist.ProtagonistConfigure Configure { get; private set; }
        [field: SerializeField] public CharacterStats BaseStats { get; private set; }
    }
}