namespace Sellswords
{
    public class StatusPhysicalDamage : Status
    {
        #region PrivateData
        
        #endregion


        #region ClassLifeCycle

        public StatusPhysicalDamage(StatusObject statusObject) : base(statusObject)
        {
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
        }

        private void Damage()
        {
            foreach (var state in States)
            {
                state.Hp -= _damage;
                if (state.Hp <= 0)
                {
                    state.IsDead = true;
                }
            }
        }

        #endregion


        #region IStatus

        public override void Use()
        {
            Damage();
        }

        #endregion
    }
}