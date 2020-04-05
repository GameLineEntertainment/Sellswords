using UnityEngine;

namespace Sellswords
{
    public class StatusSlow : Status
    {
        #region PrivateData

        private Vector3 _slowForce;

        #endregion


        #region ClassLifeCycle

        public StatusSlow(StatusObject statusObject) : base(statusObject)
        {
            _slowForce = statusObject.PushForce;
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
        }

        #endregion


        #region IStatus

        public override void Use()
        {
            // TODO: Логика работы статуса объекта с его статами (State)
            Debug.Log(nameof(StatusSlow));
        }

        #endregion
    }
}