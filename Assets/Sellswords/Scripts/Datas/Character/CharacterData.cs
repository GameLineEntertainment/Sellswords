using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "character_data", menuName = "Sellswords/Data/Character/Character Data")]
    public class CharacterData : ScriptableObject
    {
        [Header("Настройки для всех героев")]
        public CharacterSettings Settings;
        [Header("Герои")]
        public CharacterObject[] Characters;
    }
}