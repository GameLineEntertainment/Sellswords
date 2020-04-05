using UnityEngine;


namespace Sellswords
{
    public class StatusForce : Status
    {
        #region PrivateData

        #endregion


        #region ClassLifeCycle

        public StatusForce(StatusObject statusObject) : base(statusObject)
        {
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
            Force();
        }

        private void Force()
        {
            foreach (var state in States)
            {
                var force = Vector3.left * Random.Range(-_statusForce.x, _statusForce.x) +
                            (Vector3.up * _statusForce.y) +
                            (Vector3.forward * -_statusForce.z);

                foreach (var rigidbody in state.Rigidbody.transform.parent.GetComponentsInChildren<Rigidbody>())
                {
                    rigidbody.AddForce(force, ForceMode.VelocityChange);
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