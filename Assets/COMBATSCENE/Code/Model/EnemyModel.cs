using UnityEngine;

namespace CombatScene
{
    public class EnemyModel : EnemySettings, IUpdate
    {
        private PowerUI _pw;
        private bool _isPowerRefresh;

        private GameObject _charSpell;
        private Quaternion _this;
        private bool _isGetDamage;
        private bool _isStay = true;

        private void Start()
        {
            base.Start();
            LoadEditorSettings();
            _pw = PowerUI.Indicator;
            _this = transform.rotation;
        }

        public void OnEnable()
        {
            base.OnEnable();
            IsDead = false;
            _isStay = false;
            _hp = _defaultHp;
            IsDown = false;
            _anim.speed = 0.8f + _speed * 20 / 100;
        }

        public void OnDisable()
        {
            _isStay = true;
               _isSpeedChange = true;
            transform.position = _startPos;
            transform.rotation = _startRot;
            _puppet.Resurrect();
        }

        public void OnUpdate()
        {
            if (transform.position.y < _hightForDeath)
            {
                BackToPull();
            }

            if (IsDead) return;
            if (!_isStay)
            {
                if (Vector3.Distance(transform.position, _playerPlace.position) > 3)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_playerPlace.position - transform.position), Time.deltaTime * _speedRotate);
                }
                else _isStay = true;

                transform.position += transform.forward * _speed * Time.deltaTime;
            }
        }

        public void LoadEditorSettings()
        {
            _hp = InBuildSceneEditor.Main.GetEnemyHealth();
            _speed = InBuildSceneEditor.Main.GetEnemySpeed();
            _massScaler = InBuildSceneEditor.Main.GetEnemyMass();
            _wakeUpDelay = InBuildSceneEditor.Main.GetEnemyWakeUpDelay();
            _defaultHp = _hp;

            SpeedScaleAndLimit();

            MassSaler();
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetHP(float hp)
        {
            _hp = hp;
        }

        public void GetSpell(GameObject spell)
        {
            _charSpell = spell;
        }

        public void Damage(GameObject projectile, float damage)
        {
            if (_isGetDamage) return;
            _isGetDamage = true;
            if (projectile.name == Id.ToString())
            {
                _hp -= damage;
                if (!_isPowerRefresh) _pw.ChangeValue(_pw.PowerTime);
                _isPowerRefresh = true;
            }
            else
            {
                _hp -= damage / 2;
            }
            IsDown = true;
            _puppet.Kill(stateSettings);

            if (_hp <= 0)
            {
                Dead(3);
                return;
            }
            Invoke(nameof(WakeUp), _wakeUpDelay);
        }

        public void WakeUp()
        {
            _isPowerRefresh = false;
            _isGetDamage = false;
            _isStay = false;
            IsDown = false;

            _puppet.Resurrect();

        }

        public void Dead(int time)
        {
            _isSaveHp = false;
               IsDead = true;
            Invoke(nameof(BackToPull), time);
        }

        public void BackToPull()
        {
            _isReloadSettings = true;
            transform.parent.gameObject.SetActive(false);
        }

    }
}
