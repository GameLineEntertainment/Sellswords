using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "SpellManager", menuName = "Spells Manager")]
    public sealed class SpellManager : ScriptableObject
    {
        [Serializable]
        public struct SpellSettings
        {
            public int Id;
            public string Name;
            public float Speed;
            public GameObject Prefab;
            public SpellPosition MainPosition;
            public Vector3 OffsetPosition;
            public int PoolSize;
        }

        public enum SpellPosition
        {
            Up = 0,
            Left = 1,
            Right = 2,
            Forward = 3
        }

        public Vector3 GetSpellPosition(int id)
        {
            //Vector3[] pos = { new Vector3(20,10,20), new Vector3(10,10,30), new Vector3(30, 10, 30), new Vector3(20, 1.5f, 23) };
            Vector3[] pos = { new Vector3(18, 10, 25), new Vector3(13, 10, 33), new Vector3(26, 10, 33), new Vector3(20, 2f, 26) };
            return pos[id];
        }

        [Header("Characters Data")]
        public SpellSettings[] Spell;
    }
}