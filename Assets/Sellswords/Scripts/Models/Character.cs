using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Sellswords
{
    public sealed class Character : IPerson
    {
        #region PrivateData

        private Animator _animator;
        private float _attackDelay;
        private float _rotateSpeed;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private float _hp;
        private float _moveSpeed;
        private bool _canMove = true;
        private bool _onActivePosition;
        private float _stopAnimation = 0.0f;
        private float _speedAnimation = 1.0f;
        private float _moveSpeedPersent = 0.3f;
        private Vector3 _activePosition;
        private UsableServices _services;

        #endregion


        #region Fields

        public bool CanAttack;
        public Vector3 ActualPosition;
        public BaseGameType CharacterColorType;
        public SpellType Spell;

        #endregion


        #region Properties

        public int Id { get; set; }
        public State State { get; set; }
        public Transform Transform { get; set; }

        #endregion


        #region ClassLifeCycles

        public Character(CharacterObject characterObject, GameObject spawnObject, Vector3 activePosition, UsableServices services)
        {
            _services = services;
            Id = characterObject.Id;
            Spell = characterObject.UseSpell;
            CharacterColorType = characterObject.Type;
            _hp = characterObject.Health;
            _moveSpeed = characterObject.Speed;
            _attackDelay = characterObject.AttackDelay;
            var character = Object.Instantiate(characterObject.Prefab, spawnObject.transform.position,
                Quaternion.identity);
            _animator = character.GetComponent<Animator>();
            Transform = character.GetComponent<Transform>();
            ActualPosition = Transform.position;
            _activePosition = activePosition;
        }

        #endregion


        #region Methods

        public void MoveToNewPosition(Vector3 newPosition)
        {
            if (_canMove)
            {
                var position = Transform.position;
                var look = newPosition - position;
                _animator.speed = _speedAnimation + _moveSpeed * _moveSpeedPersent;
                _animator.SetFloat(Speed, _moveSpeed);
                Transform.rotation = Quaternion.Slerp(Transform.rotation, Quaternion.LookRotation(look),
                    Time.deltaTime * _rotateSpeed);
                position = Vector3.MoveTowards(position, newPosition, Time.deltaTime * _moveSpeed);
                Transform.position = position;

                if (Transform.position == newPosition)
                {
                    _animator.speed = _speedAnimation;
                    _animator.SetFloat(Speed, _stopAnimation);
                    _canMove = false;

                    if (Transform.position == _activePosition)
                    {
                        _onActivePosition = true;
                        _services.InvokeService.Invoke(Attack, _attackDelay);
                    }
                }
            }
        }

        public void Attack()
        {
            if (_onActivePosition)
            {
                CanAttack = true;
                // TODO: Сюда можно встроить анимацию
            }
        }

        public void SetCurrentCharacterPositionAsActualPosition()
        {
            ActualPosition = Transform.position;
            _canMove = true;
        }

        #endregion


        #region IPerson

        public void SetStatus(Status status)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}