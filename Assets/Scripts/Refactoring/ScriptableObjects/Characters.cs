using UnityEngine;

namespace OldSellswords
{
    [CreateAssetMenu(fileName = "Characters", menuName = "Scriptable Object/Game Characters")]
    public sealed class Characters : ScriptableObject
    {
        public CharacterContainer[] GameCharacters;
    }
}
