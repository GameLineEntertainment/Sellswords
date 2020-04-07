using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sellswords
{
    public class SpellFireTwist : Spell
    {
        #region PrivateData

        private float _maxHit;
        private List<Transform> _hitted;
        private float _timer =2;

        #endregion


        #region ClassLifeCycle

        public SpellFireTwist(Vector3 poolPosition, SpellObject spellObject, UsableServices services) : base(
            poolPosition, spellObject, services)
        {
            if (spellObject is SpellFireTwistData spell)
            {
                _maxHit = spell.maxHit;
                _hitted = new List<Transform>();
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
        }

        protected override bool HitCheck(float radius)
        {
            
            if (_isHitDone)
            {
                return false;
            }
            if (_hitted.Count >= _maxHit||_timer<0.2)
            {
                _isHitDone = true;
                ResetAfterTime();
            }

            Collider[] hits = Physics.OverlapSphere(_projectile.transform.position, radius);
            for (int i = 0; i < hits.Length; i++)
            {
                if (_targets.Any(t => t.Transform == hits[i].transform))
                {
                    if (!_hitted.Contains(hits[i].transform))
                    {
                        _hitted.Add(hits[i].transform);
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
