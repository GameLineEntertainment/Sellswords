using System.Collections.Generic;


namespace Sellswords
{
    public class GameStateController
    {
        #region Fields

        private readonly List<Controller> _fixedUpdateFeatures;
        private readonly List<Controller> _updateFeatures;
        private readonly List<Controller> _lateUpdateFeatures;

        #endregion


        #region ClassLifeCycles

        protected GameStateController(int capacity = 8)
        {
            _fixedUpdateFeatures = new List<Controller>(capacity);
            _updateFeatures = new List<Controller>(capacity);
            _lateUpdateFeatures = new List<Controller>(capacity);
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            foreach (var fixedUpdateFeature in _fixedUpdateFeatures)
            {
                fixedUpdateFeature.Initialize();
            }

            foreach (var updateFeature in _updateFeatures)
            {
                updateFeature.Initialize();
            }

            foreach (var lateUpdateFeature in _lateUpdateFeatures)
            {
                lateUpdateFeature.Initialize();
            }
        }

        public void Execute(UpdateType updateType)
        {
            List<Controller> controllers = null;
            switch (updateType)
            {
                case UpdateType.None:
                    break;
                case UpdateType.Fixed:
                    controllers = _fixedUpdateFeatures;
                    break;
                case UpdateType.Update:
                    controllers = _updateFeatures;
                    break;
                case UpdateType.Late:
                    controllers = _lateUpdateFeatures;
                    break;
                default:
                    break;
            }

            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    controller.Execute();
                }
            }
        }
        
        public void Cleanup(UpdateType updateType)
        {
            List<Controller> controllers = null;
            switch (updateType)
            {
                case UpdateType.Fixed:
                    controllers = _fixedUpdateFeatures;
                    break;

                case UpdateType.Update:
                    controllers = _updateFeatures;
                    break;

                case UpdateType.Late:
                    controllers = _lateUpdateFeatures;
                    break;

                default:
                    break;
            }
            
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    controller.Cleanup();
                }
            }
        }
        
        public void TearDown()
        {
            foreach (var fixedUpdateFeature in _fixedUpdateFeatures)
            {
                fixedUpdateFeature.TearDown();
            }

            foreach (var updateFeature in _updateFeatures)
            {
                updateFeature.TearDown();
            }

            foreach (var lateUpdateFeature in _lateUpdateFeatures)
            {
                lateUpdateFeature.TearDown();
            }
        }
        
        protected void AddFixedUpdateFeature(Controller controller)
        {
            _fixedUpdateFeatures.Add(controller);
        }
        
        protected void AddUpdateFeature(Controller controller)
        {
            _updateFeatures.Add(controller);
        }
        
        protected void AddLateUpdateFeature(Controller controller)
        {
            _lateUpdateFeatures.Add(controller);
        }

        #endregion
    }
}