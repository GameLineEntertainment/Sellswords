using System;
using UnityEngine;


namespace Sellswords
{
    [Serializable]
    public class EnemySettings
    {
        #region Fields

        [Tooltip("Префаб спавна")]
        public GameObject SpawnPosition;
        [Tooltip("Префаб пула объектов")]
        public GameObject PoolObjectPosition;

        #endregion
    }
}