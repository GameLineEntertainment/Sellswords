using System.Linq;
using UnityEngine;

namespace Sellswords
{
    public class CharactersController : IExecuteController, ICleanupController
    {
        #region PrivateData

        private readonly GameContext _context;
        private bool _isMove;
        private Character _characterWhoCanAttack;
        private UsableServices _services;

        #endregion


        #region ClassLifeCycles

        public CharactersController(GameContext context, UsableServices services)
        {
            _context = context;
            _services = services;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (_context.SwitchCharacters != SwitchCharacters.None)
            {
                SwitchHeroes(_context.SwitchCharacters);
            }

            if (_context.Characters.Any(character => character.CanAttack))
            {
                _characterWhoCanAttack = _context.Characters.First(character => character.CanAttack);
                _context.ActiveSpellObject = _context.SpellData.SpellObjects.FirstOrDefault(spell =>
                    spell.SpellColorType == _characterWhoCanAttack.CharacterColorType);
            }
        }

        #endregion


        #region ICleanupController

        public void Cleanup()
        {
            if (_context.SwitchCharacters != SwitchCharacters.None && CheckCurrentCharactersPositionOnActualPosition())
            {
                _context.SwitchCharacters = SwitchCharacters.None;
                SetCurrentCharacterPositionAsActualPosition();
            }

            if (_characterWhoCanAttack != null && _characterWhoCanAttack.CanAttack)
            {
                _characterWhoCanAttack.CanAttack = false;
            }
        }

        #endregion


        #region Methods

        private void SwitchHeroes(SwitchCharacters switchCharacters)
        {
            if (switchCharacters == SwitchCharacters.Right)
            {
                _context.Characters = new CircularLinkedList<Character>(_context.Characters.OrderBy(q => q.Id));
            }

            if (switchCharacters == SwitchCharacters.Left)
            {
                _context.Characters = new CircularLinkedList<Character>(_context.Characters.OrderByDescending(q => q.Id));
            }

            foreach (var character in _context.Characters)
            {
                character.MoveToNewPosition(_context.Characters.GetNext(character).ActualPosition);
            }
        }

        private bool CheckCurrentCharactersPositionOnActualPosition()
        {
            return _context.Characters.All(character =>
                character.Transform.position == _context.Characters.GetNext(character).ActualPosition);
        }

        private void SetCurrentCharacterPositionAsActualPosition()
        {
            foreach (var character in _context.Characters)
            {
                character.SetCurrentCharacterPositionAsActualPosition();
            }
        }

        #endregion
    }
}