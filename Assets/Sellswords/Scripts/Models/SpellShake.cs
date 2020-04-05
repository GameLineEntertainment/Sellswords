using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Sellswords
{
    public class SpellShake : Spell
    {
        #region ClassLifeCycle

        public SpellShake(Vector3 poolPosition, SpellObject spellObject, UsableServices services) : base(poolPosition, spellObject, services)
        {
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
            Starting();
            HitCheck(_hitRadius);
        }

        #endregion


        #region ISpell

        public override bool Use<T>(T target)
        {
            if (!_isFired)
            {
                _isFired = true;
                if (target is IEnumerable<IPerson> targets)
                {
                    _targets = targets.ToArray();
                    Effect();
                }
            }

            return true;
        }

        #endregion
    }
}
