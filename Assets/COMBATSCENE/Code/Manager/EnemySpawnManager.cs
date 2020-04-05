using System;
using UnityEngine;

namespace CombatScene
{
    [CreateAssetMenu(fileName = "EnemySpawnManager", menuName = "Enemy Spawn Manager")]
    public sealed class EnemySpawnManager : ScriptableObject
    {
        [Serializable]
        public struct Settings
        {
            public int MaxPoolSize;
            public bool IsClickForSpawn;
            public KeyCode SpawnButton;
            public int EnemySquadSize;
            public float StartSpawnDelay;
            public float SpawnDelay;

            public float RandomOffsetMinX;
            public float RandomOffsetMaxX;
        }

        public Settings Spawn;
    }
}