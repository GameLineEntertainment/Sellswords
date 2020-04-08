using UnityEngine;
using UnityEngine.Serialization;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "SpellData", menuName = "Sellswords/Data/Spell/FireTwist")]
    public sealed  class SpellFireTwistData : SpellObject
    { 
        public int MaxHit;
    }
}
