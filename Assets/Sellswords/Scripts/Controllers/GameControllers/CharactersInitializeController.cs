namespace Sellswords
{
    public class CharactersInitializeController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public CharactersInitializeController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion
        
        
        #region IInitializeController

        public void Initialize()
        {
            foreach (var characterObject in _context.CharacterData.Characters)
            {
                var character = new Character(characterObject,
                    _context.CharacterData.Settings.CharacterSpawnObject.transform.GetChild(characterObject.Id).gameObject,
                    _context.CharacterData.Settings.ActivePosition.position, _services);

                _context.Characters.Add(character);
            }
        }

        #endregion
    }
}