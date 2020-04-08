using UnityEngine;

namespace Sellswords
{
    [CreateAssetMenu(fileName = "SpellData", menuName = "Sellswords/Data/Spell/HammerDash")]
    public class SpellHammerDashData : SpellObject
    {
        [Tooltip("Скорость вращения")]
        public float RotateSpeed;
        public float MaxHit;
    }
}
