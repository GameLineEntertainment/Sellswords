namespace Sellswords
{
    public class GameSystemsController : GameStateController
    {
        #region ClassLifeCycles

        public GameSystemsController(GameContext context, UsableServices services)
        {
            AddUpdateFeature(new MainController(context, services));
        }

        #endregion
    }
}