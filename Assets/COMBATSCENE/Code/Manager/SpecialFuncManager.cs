using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "SpecialFuncManager", menuName = "Special Function Manager")]
    public sealed class SpecialFuncManager : ScriptableObject
    {
        [Serializable]
        public struct Settings
        {
            [Header("Slow Motion")]
            public bool IsUseSlowMotion;
            public KeyCode SlowMotionKey;
            public float SlowSpeed;
        }

        public Settings Function;
    }
}