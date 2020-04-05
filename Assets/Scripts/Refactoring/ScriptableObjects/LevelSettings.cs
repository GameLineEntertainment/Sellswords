using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable Object/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        public CharacterContainer RedCharacter;
        public CharacterContainer GreenCharacter;
        public CharacterContainer BlueCharacter;
        public EnemyContainer[] Enemies;
        public LevelQuest Quest;
    }
}