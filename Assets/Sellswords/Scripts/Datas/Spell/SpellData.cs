using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "spell_data", menuName = "Sellswords/Data/Spell/Spell Data")]
    public class SpellData : ScriptableObject
    {
        [Header("Настройки для всех способностей")]
        public SpellSettings SpellSettings;
        [Header("Способности")]
        public SpellObject[] SpellObjects;
    }
}