namespace Sellswords
{
    public abstract class Service
    {
        #region Fields
        
        protected readonly Context _contexts;

        #endregion

        
        #region ClassLifeCycles

        protected Service(Context context)
        {
            _contexts = context;
        }

        #endregion
    }
}