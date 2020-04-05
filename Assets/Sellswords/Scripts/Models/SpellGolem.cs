using System.Linq;
using UnityEngine;
using System.Collections.Generic;


namespace Sellswords
{
    public sealed class SpellGolem : Spell
    {
        #region PrivateData

        private GameObject _afterEffect;
        private float _afterEffectDelay = 0.9f;
        private float _afterEffectDmgRadius = 5;

        private Flinder[] _flinders;
        private Transform[] _savedFlinders;

        #endregion


        #region ClassLifeCycle

        public SpellGolem(Vector3 poolPosition, SpellObject spellObject, UsableServices services) : base(poolPosition, spellObject, services)
        {
            _savedFlinders = _projectile.GetComponentsInChildren<Transform>();
            _afterEffect = _projectile.transform.GetChild(_projectile.transform.childCount - 1).gameObject;
            SwitchAfterEffectState();
        }

        #endregion


        #region Methods

        protected override void Effect()
        {
            Starting();
            SaveFlinders();
            _services.InvokeService.Invoke(SwitchAfterEffectState, _afterEffectDelay);
        }

        private void SaveFlinders()
        {
            _flinders = new Flinder[_savedFlinders.Length];

            for (int i = 0; i < _flinders.Length; i++)
            {
                _flinders[i].position = _savedFlinders[i].localPosition;
                _flinders[i].rotation = _savedFlinders[i].localRotation;
                _flinders[i].scale = _savedFlinders[i].localScale;
            }
        }

        private void LoadFlinders()
        {
            for (int i = 0; i < _savedFlinders.Length; i++)
            {
                _savedFlinders[i].localPosition = _flinders[i].position;
                _savedFlinders[i].localRotation = _flinders[i].rotation;
                _savedFlinders[i].localScale = _flinders[i].scale;
            }
        }

        private void SwitchAfterEffectState()
        {
            if (_afterEffect.activeSelf)
            {
                _afterEffect.SetActive(false);
            }
            else
            {
                _afterEffect.SetActive(true);
            }
        }

        private void AfterEffect()
        {
            HitCheck(_afterEffectDmgRadius);
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

            if(_afterEffect.activeSelf)
            {
                AfterEffect();
            }

            return _afterEffect.activeSelf;
        }

        protected override void Reset()
        {
            SwitchAfterEffectState();
            LoadFlinders();
            base.Reset();
        }

        #endregion
    }
}