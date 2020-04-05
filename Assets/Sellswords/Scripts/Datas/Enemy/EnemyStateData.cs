using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "state_data", menuName = "Sellswords/Data/Enemy/State Data")]
    public class EnemyStateData : ScriptableObject
    {
        [Header("Здоровье")]
        public float Hp;
        [Header("Скорость передвижения")]
        public float MoveSpeed;
        // [Header("Физическое тело")]
        // public Rigidbody Rigidbody;
        [Header("Состояние")]
        public bool IsDead;
        [Header("Масса")]
        public float MassScale;
        [Header("Скорость поворота")]
        public float SpeedRotate;
        [Header("Задержка после падения")]
        public float WakeUpDelay;
    }
}