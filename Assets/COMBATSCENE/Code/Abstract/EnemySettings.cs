using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

namespace CombatScene
{
    public abstract class EnemySettings : MonoBehaviour
    {
        public List<EnemyModel> Enemies { get; private set; } = new List<EnemyModel>();

        protected EnemyManager.Enemy[] _enemy;
        protected EnemyManager.AllEnemy _settings;

        protected PuppetMaster _puppet;
        public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;

        protected Animator _anim;
        protected Rigidbody _rig;
        protected Renderer _render;

        protected int id;
        protected float _hp;
        protected float _defaultHp;

        protected static float _speed;
        protected static float _speedDefault;
        protected float _speedScale;
        protected float _speedLimit;

        protected float _type;
        protected float _massScaler = 1;
        protected float _wakeUpDelay;

        protected float _speedRotate = 10;
        protected bool _isSaveHp;
        protected bool _isReloadSettings;
        protected static bool _isSpeedChange;

        protected float _hightForDeath = -5;

        protected Transform _playerPlace;
        protected Vector3 _startPos;
        protected Quaternion _startRot;

        protected int enemyCount;


        public bool IsDead { get; set; }
        public bool IsDown { get; set; }

        protected List<Collider> _weapon = new List<Collider>();

        public int Id { get => id; set => id = value; }

        protected virtual void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
            _rig = GetComponentInChildren<Rigidbody>();
            _render = GetComponentInChildren<Renderer>();

            Enemies.Add(this as EnemyModel);
            _puppet = transform.parent.GetComponentInChildren<PuppetMaster>();

            _enemy = FindObjectOfType<Manager>().EnemiesSettings.Enemies;
            _settings = FindObjectOfType<Manager>().EnemiesSettings.Settings;

            _startPos = transform.position;
            _startRot = transform.rotation;

            _hp = _settings.Health;
            _defaultHp = _hp;
            _speed = _settings.Speed;
            _speedDefault = _speed;
            _speedScale = _speed * _settings.SpeedScale / 100; // не магия, а процент от числа
            _speedLimit = _speed + (_speed * _settings.SpeedLimit / 100); // не магия, а процент от числа
            _massScaler = _settings.MassScale;
            _wakeUpDelay = _settings.WakeUpDelay;
        }
        protected virtual void Start()
        {
            Settings();

            _playerPlace = GameObject.FindGameObjectWithTag("HeroSpawn").transform;
        }

        protected virtual void OnEnable()
        {
            if (_isReloadSettings) SpeedScaleAndLimit();
            if (_speed >= _speedLimit) return;
            if (_isSpeedChange)
            {
                _isSpeedChange = false;
                _speed += _speedScale;
            }
            _isReloadSettings = true;
        }

        protected virtual void Settings()
        {
            for (int i = 0; i < _enemy.Length; i++)
            {
                if (gameObject.name == _enemy[i].Prefab.name + " " + _enemy[i].Type)
                {
                    Id = (int)_enemy[i].Type;
                    if (!_settings.OneHealthForAll) _hp = _enemy[i].Health;
                    if (!_settings.OneSpeedForAll) _speed = _enemy[i].Speed;
                    _render.material = _enemy[i].Material;
                }
            }
        }

        public void MassSaler()
        {
            _massScaler = InBuildSceneEditor.Main.GetEnemyMass();
            foreach (Rigidbody rig in _puppet.GetComponentsInChildren<Rigidbody>())
            {
                rig.mass *= _massScaler;
            }
        }

        public void SpeedScaleAndLimit()
        {
            _speedScale = _speedDefault * InBuildSceneEditor.Main.GetEnemySpeedScale() / 100;
            _speedLimit = _speedDefault + (_speedDefault * InBuildSceneEditor.Main.GetEnemySpeedLimit() / 100);
        }
    }
}
