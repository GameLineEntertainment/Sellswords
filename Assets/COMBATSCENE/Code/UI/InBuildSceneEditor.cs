using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatScene
{
    public class InBuildSceneEditor : MonoBehaviour
    {
        [Header("Power Settings")]
        [SerializeField]
        private InputField _powerTime;
        [SerializeField]
        private InputField _speedStep;
        [SerializeField]
        private InputField _damageScale;
        [SerializeField]
        private InputField _smoothScale;
        [Header("Characters Settings")]
        [SerializeField]
        private InputField _health;
        [SerializeField]
        private InputField _speed;
        [SerializeField]
        private InputField _damage;
        [SerializeField]
        private InputField _attackDelay;

        [Header("Red Character")]
        [SerializeField]
        private Dropdown _redPos;
        [SerializeField]
        private InputField _redPosOffsetX;
        [SerializeField]
        private InputField _redPosOffsetY;
        [SerializeField]
        private InputField _redPosOffsetZ;
        [Header("Blue Character")]
        [SerializeField]
        private Dropdown _bluePos;
        [SerializeField]
        private InputField _bluePosOffsetX;
        [SerializeField]
        private InputField _bluePosOffsetY;
        [SerializeField]
        private InputField _bluePosOffsetZ;
        [Header("Green Character")]
        [SerializeField]
        private Dropdown _greenPos;
        [SerializeField]
        private InputField _greenPosOffsetX;
        [SerializeField]
        private InputField _greenPosOffsetY;
        [SerializeField]
        private InputField _greenPosOffsetZ;


        [Header("Enemy Spawn Settings")]
        [SerializeField]
        private Toggle _isUseButtonSpawn;
        [SerializeField]
        private InputField _squadSize;
        [SerializeField]
        private InputField _startDelay;
        [SerializeField]
        private InputField _spawnDelay;
        [SerializeField]
        private InputField _offsetSpawnMinX;
        [SerializeField]
        private InputField _offsetSpawnMaxX;
        [Header("Enemy Settings")]
        [SerializeField]
        private InputField _enemyHealth;
        [SerializeField]
        private InputField _enemyMass;
        [SerializeField]
        private InputField _enemySpeed;
        [SerializeField]
        private InputField _enemySpeedScale;
        [SerializeField]
        private InputField _enemySpeedLimit;
        [SerializeField]
        private InputField _wakeUpDelay;
        [SerializeField]
        private Toggle _isAddForceRagdoll;
        [SerializeField]
        private InputField _isCorrectEnemy;
        [SerializeField]
        private InputField _isWrongEnemy;



        private Vector3 _redPosVect;
        private Vector3 _bluePosVect;
        private Vector3 _greenPosVect;

        private bool _isCanCnahge;

        private CharManager.CharSettings _charSet;
        private EnemyManager.Enemy[] _enemySet;
        private EnemyManager.AllEnemy _ragdollSet;
        private EnemySpawnManager.Settings _enemySpawn;

        private List<CharacterModel> _characters = new List<CharacterModel>();

        public static InBuildSceneEditor Main;

        private void Awake()
        {
            Main = this;
        }

        public void Start()
        {
            _powerTime.text = PowerUI.Indicator.PowerTime.ToString();
            _speedStep.text = PowerUI.Indicator.StepStreak.ToString();
            _damageScale.text = PowerUI.Indicator.Damage.ToString();
            _smoothScale.text = PowerUI.Indicator.LerpSmooth.ToString();

            _charSet = FindObjectOfType<Manager>().CharactersSettings.Settings;
            _health.text = _charSet.Health.ToString();
            _speed.text = _charSet.Speed.ToString();
            _damage.text = _charSet.Damage.ToString();
            _attackDelay.text = _charSet.AttackDelay.ToString();

            _ragdollSet = FindObjectOfType<Manager>().EnemiesSettings.Settings;
            _enemySet = FindObjectOfType<Manager>().EnemiesSettings.Enemies;
            _enemySpawn = FindObjectOfType<Manager>().EnemySpawnSettings.Spawn;

            _isUseButtonSpawn.isOn = _enemySpawn.IsClickForSpawn;
            _squadSize.text = _enemySpawn.EnemySquadSize.ToString();
            _startDelay.text = _enemySpawn.StartSpawnDelay.ToString();
            _spawnDelay.text = _enemySpawn.SpawnDelay.ToString();
            _offsetSpawnMinX.text = _enemySpawn.RandomOffsetMinX.ToString();
            _offsetSpawnMaxX.text = _enemySpawn.RandomOffsetMaxX.ToString();

            _enemyHealth.text = _ragdollSet.Health.ToString();
            _enemyMass.text = _ragdollSet.MassScale.ToString();
            _enemySpeed.text = _ragdollSet.Speed.ToString();
            _enemySpeedScale.text = _ragdollSet.SpeedScale.ToString();
            _enemySpeedLimit.text = _ragdollSet.SpeedLimit.ToString();
            _wakeUpDelay.text = _ragdollSet.WakeUpDelay.ToString();


            foreach (CharacterModel player in FindObjectsOfType<CharacterModel>())
            {
                _characters.Add(player);
            }

            _isCanCnahge = true;

            _isAddForceRagdoll.transform.parent.GetChild(1).gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Restart();
            }
        }

        public float GetFloat(string text)
        {
                float result;
                float.TryParse(text, out result);
                return result;
        }

        public void SetPowerSettings()
        {
            PowerUI.Indicator.NewSettings(GetFloat(_powerTime.text), 
                                          GetFloat(_speedStep.text), 
                                          GetFloat(_damageScale.text),
                                          GetFloat(_smoothScale.text));
        }

        public void SetCharSettings()
        {
            foreach (CharacterModel player in _characters)
            {
                player.MainSettings(GetFloat(_health.text),
                                    GetFloat(_speed.text), 
                                    GetFloat(_damage.text), 
                                    GetFloat(_attackDelay.text));
            }

        }

        public void SetEnemySettings()
        {
            foreach (EnemyModel enemy in FindObjectsOfType<EnemyModel>())
            {
                foreach (EnemyModel en in enemy.Enemies) en.LoadEditorSettings();
            }
        }

        public void SetSpellPos()
        {
            foreach (CharacterModel player in _characters)
            {
                switch (player.GetID())
                {
                    case 0:
                        {
                            player.SpellMainPos(0, _redPos.value);
                            break;
                        }
                    case 1:
                        {
                            player.SpellMainPos(1, _greenPos.value);
                            break;
                        }
                    case 2:
                        {
                            player.SpellMainPos(2, _bluePos.value);
                            break;
                        }
                }
            }
        }
        public void SetSpellOffsetPos()
        {
            _redPosVect = new Vector3(GetFloat(_redPosOffsetX.text), GetFloat(_redPosOffsetY.text), GetFloat(_redPosOffsetZ.text));
            _bluePosVect = new Vector3(GetFloat(_bluePosOffsetX.text), GetFloat(_bluePosOffsetY.text), GetFloat(_bluePosOffsetZ.text));
            _greenPosVect = new Vector3(GetFloat(_greenPosOffsetX.text), GetFloat(_greenPosOffsetY.text), GetFloat(_greenPosOffsetZ.text));

            foreach (CharacterModel player in _characters)
            {
                switch (player.GetID())
                {
                    case 0:
                        {
                            player.SpellOffsetPos(_redPosVect);
                            break;
                        }
                    case 1:
                        {
                            player.SpellOffsetPos(_greenPosVect);
                            break;
                        }
                    case 2:
                        {
                            player.SpellOffsetPos(_bluePosVect);
                            break;
                        }
                }
            }
        }


        public void GetSpellPlace(int charId, int placeId)
        {
            if (charId == 0) _redPos.value = placeId;
            else if (charId == 1) _greenPos.value = placeId;
            else if (charId == 2) _bluePos.value = placeId;
        }

        public void GetSpellPlaceOffset(int charId, Vector3 offset)
        {
            if (charId == 0)
            {
                _redPosOffsetX.text = offset.x.ToString();
                _redPosOffsetY.text = offset.y.ToString();
                _redPosOffsetZ.text = offset.z.ToString();
            }
            else if (charId == 1)
            {
                _greenPosOffsetX.text = offset.x.ToString();
                _greenPosOffsetY.text = offset.y.ToString();
                _greenPosOffsetZ.text = offset.z.ToString();
            }
            else if (charId == 2)
            {
                _bluePosOffsetX.text = offset.x.ToString();
                _bluePosOffsetY.text = offset.y.ToString();
                _bluePosOffsetZ.text = offset.z.ToString();
            }
        }

        public void SetEnemySpawnSettings()
        {
            if (!_isCanCnahge) return;

            EnemySpawner.main.SetNewSettings(_isUseButtonSpawn.isOn,
                                             (int)GetFloat(_squadSize.text),
                                             GetFloat(_startDelay.text),
                                             GetFloat(_spawnDelay.text),
                                             GetFloat(_offsetSpawnMinX.text),
                                             GetFloat(_offsetSpawnMaxX.text));
            DeleteEnemies();
        }

        public float GetEnemyHealth()
        {
            return GetFloat(_enemyHealth.text);
        }
        public float GetEnemyMass()
        {
            return GetFloat(_enemyMass.text);
        }
        public float GetEnemySpeed()
        {
            return GetFloat(_enemySpeed.text);
        }
        public float GetEnemySpeedScale()
        {
            return GetFloat(_enemySpeedScale.text);
        }
        public float GetEnemySpeedLimit()
        {
            return GetFloat(_enemySpeedLimit.text);
        }
        public float GetEnemyWakeUpDelay()
        {
            return GetFloat(_wakeUpDelay.text);
        }
        public bool GetRagdollState()
        {   
            return _isAddForceRagdoll.isOn;
        }
        public float GetTrueForce()
        {
            return GetFloat(_isCorrectEnemy.text);
        }
        public float GetWrongForce()
        {
            return GetFloat(_isWrongEnemy.text);
        }


        public void DeleteEnemies()
        {
            foreach (EnemyModel enemy in FindObjectsOfType<EnemyModel>())
            {
                foreach (EnemyModel en in enemy.Enemies)
                {
                    enemy.LoadEditorSettings();
                    enemy.Dead(0);
                }
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
