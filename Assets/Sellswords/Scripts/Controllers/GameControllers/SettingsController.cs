namespace Sellswords
{
    public class SettingsController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public SettingsController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _context.CharacterData = Data.CharacterData;
            _context.EnemyData = Data.EnemyData;
            _context.SpellData = Data.SpellData;
        }

        #endregion
    }
}