using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "UIManager", menuName = "UI Manager")]
    public sealed class UIManager : ScriptableObject
    {
        [Serializable]
        public struct Element
        {
            public string Name;
            public float Value;
        }

        [Header("UI Elements")]
        public Element[] UI;
    }
}