namespace Sellswords
{
    public sealed class MainController : Controller
    {
        #region ClassLifeCycles

        public MainController(GameContext context, UsableServices services)
        {
            Add(new SettingsController(context, services));
            Add(new InputController(context, services));
            Add(new CharactersInitializeController(context, services));
            Add(new CharactersController(context, services));
            Add(new EnemySpawnController(context, services));
            Add(new EnemiesController(context, services));
            Add(new SpellsSpawnController(context, services));
            Add(new SpellsController(context, services));
        }

        #endregion
    }
}