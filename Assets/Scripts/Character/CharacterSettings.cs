using System;
using UnityEngine;

namespace Character
{
    [Serializable]
    public sealed class CharacterSettings
    {
        [field: SerializeField] public GameObject CharacterPrefab { get; private set; }
        [field: SerializeField] public string JumpKey { get; private set; } = "space";
        [field: SerializeField] public string LeftKey { get; private set; } = "left";
        [field: SerializeField] public string RightKey { get; private set; } = "right";
        [field: SerializeField] public bool UsePhysics { get; private set; } = true;
        [field: SerializeField] public int Speed { get; private set; } = 125;
        [field: SerializeField] public float MovementForce { get; private set; } = 1250;
        [field: SerializeField] public float JumpForce { get; private set; } = 1000;
        [field: SerializeField] public int JumpHeight { get; private set; } = 500;
        [field: SerializeField] public int JumpThreshold { get; private set; } = 100;
        [field: SerializeField] public float LeftLimit { get; private set; } = -854;
        [field: SerializeField] public float RightLimit { get; private set; } = 854;
    }
}