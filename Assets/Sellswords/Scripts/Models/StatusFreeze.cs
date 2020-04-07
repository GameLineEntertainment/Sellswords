using UnityEngine;

namespace Sellswords
{
    public class StatusFreeze : Status
    {
        #region PrivateData

        private float _speed;
        private float _rotate;
        private bool _isFreeze;

        #endregion


        #region ClassLifeCycle

        public StatusFreeze(StatusObject statusObject) : base(statusObject)
        {
        }

        #endregion


        #region Methods

        public override void Reset()
        {
            foreach (var states in States)
            {
                if (states is EnemyState state && state.IsFreeze)
                {
                    state.MoveSpeed = _speed;
                    state.SpeedRotate = _rotate;
                    state.IsFreeze = false;
                }
            }
        }


        protected override void Effect()
        {
            Freeze();
        }

        private void Freeze()
        {
            foreach (var states in States)
            {
                if (states is EnemyState state && !state.IsFreeze)
                {
                    _speed = state.MoveSpeed;
                    _rotate = state.SpeedRotate;
                    state.MoveSpeed = 0;
                    state.SpeedRotate = 0;
                    state.IsFreeze = true;
                }
            }
        }

        #endregion


        #region IStatus

        public override void Use()
        {
            Effect();
        }

        #endregion
    }
}
