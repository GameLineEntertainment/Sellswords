using System;
using UnityEngine;


namespace Sellswords
{
    [Serializable]
    public struct CharacterSettings
    {
        #region Fields

        [Tooltip("Префаб с позициями спавна героев")]
        public GameObject CharacterSpawnObject;
        [Tooltip("Позиция героя для атаки")]
        public Transform ActivePosition;

        #endregion
    }
}