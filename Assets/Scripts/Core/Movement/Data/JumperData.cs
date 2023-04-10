using System;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class JumperData
    {
        [field: SerializeField] public float JumpForce { get; private set; }
        
        [field: SerializeField] public int GravityScale { get; private set; }
        
    }
}