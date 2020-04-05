using System.Collections.Generic;
using UnityEngine;

namespace Sellswords
{
    public abstract class Status : IStatus
    {
        #region Fields

        public float Time { get; set; }
        public float Interval { get; set; }
        protected List<State> States { get; set; }

        protected Vector3 _statusForce;
        protected float _damage;

        #endregion


        #region ClassLifeCycle

        public Status(StatusObject statusObject)
        {
            States = new List<State>();
            Time = statusObject.Duration;
            Interval = statusObject.Interval;
            _statusForce = statusObject.PushForce;
            _damage = statusObject.Damage;
        }

        #endregion


        #region Methods

        // TODO: Сброс параметров статов (State)
        public virtual void Reset()
        {
            States.Clear();
        }

        // TODO: Внешний вид статуса
        protected abstract void Effect();

        public void SetState(State state)
        {
            States.Add(state);
        }

        #endregion


        #region IStatus

        public abstract void Use();

        #endregion
    }
}