using Protagonist.Configures;
using System;
using UnityEngine;

namespace Protagonist
{
    [Serializable]
    public class ProtagonistConfigure
    {
        [field: SerializeField] public ProtagonistControllerConfigure ControllerConfigure { get; private set; }
    }
}