using System;
using UnityEngine;


namespace Sellswords
{
    [Serializable]
    public class EnemyObject
    {
        #region Fields

        public int Id;
        [Tooltip("Тип")]
        public BaseGameType Type;
        [Tooltip("Префаб")]
        public GameObject Prefab;
        [Tooltip("Материал")]
        public Material Material;

        public EnemyStateData EnemyStateData;

        #endregion
    }
}