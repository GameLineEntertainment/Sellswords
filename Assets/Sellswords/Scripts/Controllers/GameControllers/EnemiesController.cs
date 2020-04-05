namespace Sellswords
{
    public class EnemiesController : IExecuteController
    {
        #region Fields

        private GameContext _context;
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public EnemiesController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            // TODO: Обрабатывать поведение врагов после появления
            foreach (var enemy in _context.Enemies)
            {
                enemy.Move();
            }
        }

        #endregion
    }
}