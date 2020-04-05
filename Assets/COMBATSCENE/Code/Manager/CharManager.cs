using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "CharManager", menuName = "Characters Manager")]
    public sealed class CharManager : ScriptableObject
    {
        [Serializable]
        public struct CharSettings
        {
            public float Health;
            public float Damage;
            public float Speed;
            public float AttackDelay;
        }
        [Serializable]
        public struct Char
        {
            public CharacterType Type;
            public GameObject Prefab;
            public int UseSpellId;
            public Vector3 SpellPosOffset;
        }

        [Header("For All Characters")]
        public CharSettings Settings;
        [Header("Characters Data")]
        public Char[] Characters;
    }
}