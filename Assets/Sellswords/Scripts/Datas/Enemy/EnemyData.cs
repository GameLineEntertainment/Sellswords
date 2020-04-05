using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "enemy_data", menuName = "Sellswords/Data/Enemy/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        #region Fields

        [Header("Настройки для всех врагов")]
        public EnemySettings Settings;
        [Header("Враги")]
        public EnemyObject[] Enemies;

        #endregion
    }
}