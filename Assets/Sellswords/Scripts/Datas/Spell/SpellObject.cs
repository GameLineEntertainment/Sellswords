﻿using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "spell_object_data", menuName = "Sellswords/Data/Spell/Spell Object")]
    public class SpellObject : ScriptableObject
    {
        public int Id;
        public int PoolSize;
        public BaseGameType SpellColorType;
        public SpellType SpellType;
        public GameObject Spell;
        public float Speed;
        public float HitRadius;
        public float DestroyAfterTime;
        public bool MustSpawnOnEnemyPosition;
        public Transform SpawnSpellPosition;
        public StatusObject[] StatusObjects;
    }
}