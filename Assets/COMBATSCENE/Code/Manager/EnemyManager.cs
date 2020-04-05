using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "EnemyManager", menuName = "Enemy Manager")]
    public sealed class EnemyManager : ScriptableObject
    {
        [Serializable]
        public struct AllEnemy
        {
            public bool OneHealthForAll;
            public float Health;
            public bool OneSpeedForAll;
            public float Speed;
            public float SpeedScale;
            public float SpeedLimit;
            public float MassScale;
            public float WakeUpDelay;
        }

        [Serializable]
        public struct Enemy
        {
            public CharacterType Type;
            public GameObject Prefab;
            public Material Material;
            public float Health;
            public float Speed;
        }

        [Header("Settings For All Enemies")]
        public AllEnemy Settings;
        [Header("Enemies Data")]
        public Enemy[] Enemies;
    }
}