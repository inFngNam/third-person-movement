using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protagonist.Configures
{
    [Serializable]
    public class ProtagonistControllerConfigure
    {
        [field: SerializeField] public bool MoveRelativeToCamera { get; private set; }
    }
}