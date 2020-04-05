using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatScene
{
    public abstract class ProjectileSettings
    {
        public SpellManager.SpellSettings[] Spell =  Object.FindObjectOfType<Manager>().SpellsSettings.Spell;

        protected GameObject _player;
        protected GameObject _gameObject;
        protected Transform _transform;
        protected Rigidbody _rigibody;

        protected Vector3 _targetOffset = Vector3.up;

        protected Transform _target;
        protected float _distance;
        protected float _damage;
        protected float _speed;
        protected int _index;
        protected int _maxIndex;

        protected bool _isScaleSpeed;

        protected List<Transform> _enemies = new List<Transform>();
        protected List<Transform> _targetList = new List<Transform>();

        public virtual void SetDamage(float dmg)
        {
            _damage = dmg;
        }

        public void ScanEnemy()
        {
            _enemies.Clear();
            foreach (EnemyModel enemy in Object.FindObjectsOfType<EnemyModel>())
            {
                if (!enemy.IsDead || !enemy.IsDown)
                {
                    _enemies.Add(enemy.transform);
                }
            }

            var SortedList = _enemies.OrderBy(dis => dis.position.z - _player.transform.position.z).ToList();
            _target = SortedList[0];

            if (_target != null)
            {
                if (_transform.position.y > 4)
                {
                    _speed += _target.GetComponent<EnemyModel>().GetSpeed() * 5;
                    _transform.LookAt(_target.position + _targetOffset);
                }

            }
        }
    }
}
