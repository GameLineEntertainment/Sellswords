namespace Sellswords
{
    public sealed class UsableServices
    {
        #region Fields
        
        public static readonly UsableServices SharedInstance = new UsableServices();
        
        #endregion
        
        
        #region Properties
        
//        public PhysicsService PhysicsService { get; private set; }
        public UnityTimeService UnityTimeService { get; private set; }
        public InvokeService InvokeService { get; private set; }
        
        #endregion
        
        
        #region Methods
        
        public void Initialize(Context context)
        {
//            PhysicsService = new PhysicsService(context);
            UnityTimeService = new UnityTimeService(context);
            InvokeService = new InvokeService(context);
        }

        public void Update()
        {
            InvokeService.Update();
        }

        public void Cleanup()
        {
            InvokeService.CleanUp();
        }
        
        #endregion
    }
}