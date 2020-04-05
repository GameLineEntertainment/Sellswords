using System.Collections.Generic;


namespace Sellswords
{
    public abstract class Controller : IInitializeController, IExecuteController, ICleanupController, ITearDownController
    {
        #region Fields

        protected readonly List<IInitializeController> InitializeControllers;
        protected readonly List<IExecuteController> ExecuteControllers;
        protected readonly List<ICleanupController> CleanupControllers;
        protected readonly List<ITearDownController> TearDownControllers;

        #endregion


        #region ClassLifeCycles

        protected Controller()
        {
            InitializeControllers = new List<IInitializeController>();
            ExecuteControllers = new List<IExecuteController>();
            CleanupControllers = new List<ICleanupController>();
            TearDownControllers = new List<ITearDownController>();
        }

        #endregion


        #region Methods

        protected virtual Controller Add(IController controller)
        {
            if (controller is IInitializeController initializeController)
            {
                InitializeControllers.Add(initializeController);
            }

            if (controller is IExecuteController executeController)
            {
                ExecuteControllers.Add(executeController);
            }
            
            if (controller is ICleanupController cleanupController)
            {
                CleanupControllers.Add(cleanupController);
            }
            
            if (controller is ITearDownController tearDownController)
            {
                TearDownControllers.Add(tearDownController);
            }

            return this;
        }

        #endregion


        #region IInitializeController

        public virtual void Initialize()
        {
            foreach (var controller in InitializeControllers)
            {
                controller.Initialize();
            }
        }

        #endregion


        #region IExecuteController

        public virtual void Execute()
        {
            foreach (var controller in ExecuteControllers)
            {
                controller.Execute();
            }
        }

        #endregion


        #region ICleanupController

        public void Cleanup()
        {
            foreach (var controller in CleanupControllers)
            {
                controller.Cleanup();
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            foreach (var controller in TearDownControllers)
            {
                controller.TearDown();
            }
        }

        #endregion
    }
}