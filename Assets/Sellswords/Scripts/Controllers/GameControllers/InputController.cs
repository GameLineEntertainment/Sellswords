using UnityEngine;


namespace Sellswords
{
    public class InputController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public InputController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion
        
        
        #region IExecuteController

        public void Execute()
        {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _context.SwitchCharacters = SwitchCharacters.Left;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _context.SwitchCharacters = SwitchCharacters.Right;
                }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _context.NeedSpawnEnemy = true;
            }
        }

        #endregion
    }
}