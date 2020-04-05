using System;


namespace Sellswords
{
    public abstract class Invoke
    {

        #region Fields

        public readonly Action Method;

        #endregion


        #region ClassLifeCycles

        protected Invoke(Action method)
        {
            Method = method;
        }

        #endregion
    }
}