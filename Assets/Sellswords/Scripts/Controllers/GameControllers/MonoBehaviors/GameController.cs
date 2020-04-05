using UnityEngine;


namespace Sellswords
{
    public class GameController : MonoBehaviour
    {
        #region Fields

        private GameStateController _gameController;
        private UsableServices _services;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            
        }

        private void Start()
        {
            var context = new GameContext();
            _services = UsableServices.SharedInstance;
            _services.Initialize(context);
            
            _gameController = new GameSystemsController(context, _services);
            _gameController.Initialize();
        }

        private void FixedUpdate()
        {
            _gameController.Execute(UpdateType.Fixed);
            _gameController.Cleanup(UpdateType.Fixed);
        }

        private void Update()
        {
            _services.Update();
            _services.Cleanup();
            
            _gameController.Execute(UpdateType.Update);
            _gameController.Cleanup(UpdateType.Update);
        }

        private void LateUpdate()
        {
            _gameController.Execute(UpdateType.Late);
            _gameController.Cleanup(UpdateType.Late);
        }

        private void OnDestroy()
        {
            _gameController.TearDown();
        }

        #endregion
    }
}