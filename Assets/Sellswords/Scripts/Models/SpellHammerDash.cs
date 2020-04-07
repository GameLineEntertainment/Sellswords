using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Sellswords
{
    public class SpellHammerDash : Spell
    {
        #region PrivateData

        private float _maxHit;
        private List<Transform> hitted;
        private float _rotateSpeed;
        private float _timer = 3;

        #endregion


        #region ClassLifeCycle

        public SpellHammerDash(Vector3 poolPosition, SpellObject spellObject, UsableServices services) : base(
            poolPosition, spellObject, services)
        {
            if (spellObject is SpellHammerDashData spell)
            {
                _maxHit = spell.MaxHit;
                hitted = new List<Transform>();
                _rotateSpeed = spell.RotateSpeed;
            }
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
            Starting();
            _projectile.transform.LookAt(_targets.FirstOrDefault().Transform);
            Move();
        }

        protected override void Move()
        {
            _projectile.GetComponent<Rigidbody>().velocity = Vector3.forward * _speed;
            _projectile.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, _rotateSpeed, 0);
        }

        protected override bool HitCheck(float radius)
        {
            if (_isHitDone)
            {
                return false;
            }

            if (hitted.Count >= _maxHit || _timer < 0.2)
            {
                _isHitDone = true;
                ResetAfterTime();
            }

            Collider[] hits = Physics.OverlapSphere(_projectile.transform.position, radius);
            for (int i = 0; i < hits.Length; i++)
            {
                if (_targets.Any(t => t.Transform == hits[i].transform))
                {
                    if (!hitted.Contains(hits[i].transform))
                    {
                        hitted.Add(hits[i].transform);
                        var target = _targets.FirstOrDefault(t => t.Transform == hits[i].transform);
                        HitEffect(target);
                    }
                }
            }

            return _isHitDone;
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

            _timer -= _services.UnityTimeService.DeltaTime();
            return HitCheck(_hitRadius);
        }

        #endregion
    }
}
