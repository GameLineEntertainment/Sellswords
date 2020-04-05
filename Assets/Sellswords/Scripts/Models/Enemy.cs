using System.Linq;
using RootMotion.Dynamics;
using UnityEngine;


namespace Sellswords 
{
    public sealed class Enemy : IPerson
    {
        #region Fields

        public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;

        #endregion


        #region PrivateData

        private bool _isPowerRefresh;
        private GameObject _charSpell;
        private Quaternion _this;
        private bool _isGetDamage;
        private Animator _animator;
        private float _defaultHp;
        private Transform _playerPlace;
        private Vector3 _startPos;
        private Quaternion _startRot;
        private Renderer _render;
        private PuppetMaster _puppet;
        private bool _isCanDown;
        private UsableServices _services;
        private EnemyStateData _data;

        #endregion


        #region Properties

        public int Id { get; set; }
        public State State { get; set; }
        public Transform Transform { get; set; }

        #endregion


        #region ClassLifeCycles

        public Enemy(EnemyObject enemyObject, GameObject spawnPosition, Transform playerPlace, UsableServices services)
        {
            Id = enemyObject.Id;
            var enemy = Object.Instantiate(enemyObject.Prefab, spawnPosition.transform.position,
                spawnPosition.transform.rotation); // перенимаю поворот спавна
            enemy = enemy.transform.GetChild(enemy.transform.childCount - 1).gameObject;
            State = new EnemyState(enemyObject.EnemyStateData, enemy.GetComponent<Rigidbody>());
            _defaultHp = State.Hp;
            _playerPlace = playerPlace;
            Transform = enemy.transform;
            _animator = enemy.GetComponent<Animator>();
            _puppet = Transform.parent.GetComponentInChildren<PuppetMaster>();
            _render = enemy.GetComponentsInChildren<Renderer>().First();
            _render.material = enemyObject.Material;
            _services = services;
        }

        #endregion


        #region Methods

        public void Move()
        {
            if (State.IsDead) return;

            if (Transform.position.CalcDistance(_playerPlace.position) < 3) // сменил знак
            {
                Transform.rotation = Quaternion.Slerp(Transform.rotation,
                    Quaternion.LookRotation(_playerPlace.position - Transform.position), Time.deltaTime * ((EnemyState)State).SpeedRotate);
            }

            // TODO: Почему простая телепортация, а не физика?
            Transform.position += Time.deltaTime * State.MoveSpeed * Transform.forward;
        }

        public void WakeUp()
        {
            _isPowerRefresh = false;
            _isGetDamage = false;
            _isCanDown = true;
            _puppet.Resurrect();
        }

        public void Dead(int time)
        {
            State.IsDead = true;
        }

        #endregion


        #region IPerson

        public void SetStatus(Status status)
        {
            status.SetState(State);
            _services.InvokeService.InvokeRepeating(status.Use, status.Time, status.Interval, status.Reset);
        }

        #endregion
    }
}