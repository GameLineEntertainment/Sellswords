using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatScene
{
    public class CharacterModel : CharacterSettings, IUpdate
    {
        public bool IsMove { set; get; }
        private bool _isCanAttack;
        private int _staySpeed = 0;
        private float _rotateSpeed = 10;


        private Vector3 _newPosition;

        private void Start()
        {
            base.Start();
            BasicSpeed = Speed;
            _anim.SetFloat("Speed", _staySpeed);
            PlayerController.Player[transform.GetSiblingIndex()] = gameObject.GetComponent<CharacterModel>();
        }
        public void OnUpdate()
        {
            if (IsMove) Moving();
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * _rotateSpeed);
            }
        }

        public int GetID()
        {
            return _id;
        }

        public void Move(Vector3 newPos, bool isActivePlace)
        {
            _isCanAttack = isActivePlace;
            _anim.speed = 1 + Speed * 30 / 100;
            IsMove = true;
            _newPosition = newPos;
            _anim.SetFloat("Speed", Speed);
        }

        public void Moving()
        {
            Vector3 look = _newPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), Time.deltaTime * _rotateSpeed);
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime * Speed);

            if (transform.position == _newPosition)
            {
                _anim.speed = 1;
                _anim.SetFloat("Speed", 0);
                IsMove = false;

                if (_isCanAttack) Invoke("Attack", _attackDelay);
            }
        }

        public void Attack()
        {
            if (!EnemyList()) return;
            if (!_isCanAttack) return;
            
            Meteor.Main.Starting(_id, Damage, ProjectilesSettings[_id].Speed, _spellPlace);
        }
    }
}
