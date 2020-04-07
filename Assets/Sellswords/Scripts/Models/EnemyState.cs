using UnityEngine;


namespace Sellswords
{
    public class EnemyState : State
    {
        #region Fields

        public float MassScale;
        public float SpeedRotate;
        public float WakeUpDelay;
        public bool IsFreeze;

        #endregion


        #region ClassLifeCycles

        public EnemyState(EnemyStateData data, Rigidbody rigidbody)
        {
            Hp = data.Hp;
            MoveSpeed = data.MoveSpeed;
            SpeedRotate = data.SpeedRotate;
            IsDead = data.IsDead;
            MassScale = data.MassScale;
            WakeUpDelay = data.WakeUpDelay;
            Rigidbody = rigidbody;
            IsFreeze = false;
        }

        #endregion
    }
}