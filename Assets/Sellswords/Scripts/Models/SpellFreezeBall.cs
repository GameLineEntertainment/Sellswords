using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sellswords
{
    public sealed class SpellFreezeBall : Spell
    {
        #region PrivateData

        private GameObject _afterEffect;
        private GameObject _mainEffect;
        private Rigidbody _rigidbody;
        private float _afterEffectTime = 2;
        private float _freezeDuration;
        private float _speed;

        #endregion


        #region ClassLifeCycle

        public SpellFreezeBall(Vector3 poolPosition, SpellObject spellObject, UsableServices services) : base(
            poolPosition, spellObject, services)
        {
            if (spellObject is SpellFreezeBallData spell)
            {
                _speed = spell.Speed;
                _afterEffect = _projectile.transform.GetChild(_projectile.transform.childCount - 1).gameObject;
                _mainEffect = _projectile.transform.GetChild(0).gameObject;
            }

            _rigidbody = spellObject.Spell.GetComponent<Rigidbody>();
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
            Starting();
            _projectile.transform.LookAt(_targets.FirstOrDefault().Transform);
            var rotation = _projectile.transform.rotation;
            rotation.Set(rotation.x, 0, rotation.z, rotation.w);
            Move();
        }


        private void AfterEffect()
        {
            _projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _afterEffect.SetActive(true);
            _mainEffect.SetActive(false);
        }

        protected override void Move()
        {
            _projectile.GetComponent<Rigidbody>().velocity = Vector3.forward * _speed;
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


            if (HitCheck(_hitRadius))
            {
                AfterEffect();
                ResetAfterTime(_afterEffectTime);
            }

            return _isHitDone;
        }

        protected override void Reset()
        {
            _afterEffect.SetActive(false);
            _mainEffect.SetActive(true);
            base.Reset();
        }

        #endregion
    }
}
