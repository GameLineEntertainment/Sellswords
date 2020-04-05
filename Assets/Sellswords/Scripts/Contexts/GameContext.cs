using System;
using System.Collections.Generic;


namespace Sellswords
{
    public sealed class GameContext : Context
    {
        #region Fields

        public CharacterData CharacterData;
        public EnemyData EnemyData;
        public SpellData SpellData;
        public SwitchCharacters SwitchCharacters = SwitchCharacters.None;
        public bool NeedSpawnEnemy = true;
        public bool SpawnEnemy = false;
        public CircularLinkedList<Character> Characters = new CircularLinkedList<Character>();
        public IEnumerable<Enemy> Enemies = new List<Enemy>();
        public SpellObject ActiveSpellObject;
        public ISpell ActiveSpell;
        public Action ActiveAttack;
        public PoolObject<ISpell> Spells;

        #endregion
    }
}