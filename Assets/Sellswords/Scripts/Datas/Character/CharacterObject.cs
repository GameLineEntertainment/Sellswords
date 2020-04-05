using System;
using UnityEngine;


namespace Sellswords
{
    [Serializable]
    public struct CharacterObject
    {
        #region Fields

        public int Id;
        [Tooltip("Здоровье")]
        public float Health;
//        [Tooltip("Урон")]
//        public float Damage;
        [Tooltip("Скорость передвижения")]
        public float Speed;
        [Tooltip("Задержка атаки")]
        public float AttackDelay;
        [Tooltip("Тип героя")]
        public BaseGameType Type;
        [Tooltip("Префаб героя")]
        public GameObject Prefab;
        public SpellType UseSpell; // Временно для тестов со спелами пока нет офф реализации
//        public Vector3 SpellPosOffset;

        #endregion
    }
}