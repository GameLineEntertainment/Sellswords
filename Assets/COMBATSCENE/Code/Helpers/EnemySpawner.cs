using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatScene
{
    public class EnemySpawner : IAwake, IUpdate
    {
        public static EnemySpawner main;

        private EnemyManager.Enemy[] _enemy;
        private EnemySpawnManager.Settings _settings;
        private Transform _spawnPlace;
        private Vector3 _offsetSpawn;

        private int _poolSize;
        private bool _isClickForSpawn;
        private float _randomOffsetMinX;
        private float _randomOffsetMaxX;
        private float _startDelayDefault;
        private float _spawnDelayDefault;

        private int _enemyIndex;
        private int _squadSize;
        private int _activeEnemy;
        private float _timer = 0f;
        private float _firstDelayTimer = 0f;
        private bool _isCanSpawn;
        private bool _targetSpawn;
        private bool _isFull;
        private bool _isHide;

        private int _rndStreak;
        private int _lastType = -1;

        private Pool _pool;

        private List<GameObject> _allTypeEnemies = new List<GameObject>();
        public static GameObject[] Enemies { get; private set; }


        public void OnAwake()
        {
            main = this;
            _pool = new Pool();
            _offsetSpawn = Vector3.zero;
            _spawnPlace = GameObject.FindGameObjectWithTag("EnemySpawn").transform;
            _settings = Object.FindObjectOfType<Manager>().EnemySpawnSettings.Spawn;
            _enemy = Object.FindObjectOfType<Manager>().EnemiesSettings.Enemies;

            foreach (EnemyManager.Enemy en in _enemy)
            {
                _allTypeEnemies.Add(en.Prefab);
            }

            _poolSize = _settings.MaxPoolSize;
            _squadSize = _settings.EnemySquadSize;
            _startDelayDefault = _settings.StartSpawnDelay;
            _spawnDelayDefault = _settings.SpawnDelay;
            _randomOffsetMinX = _settings.RandomOffsetMinX;
            _randomOffsetMaxX = _settings.RandomOffsetMaxX;
            _isClickForSpawn = _settings.IsClickForSpawn;

            Enemies = new GameObject[_poolSize];

            Enemies = _pool.StartPool(_allTypeEnemies, _poolSize, _spawnPlace.position, _spawnPlace.rotation, _allTypeEnemies.Count);
            Settings();
        }

        public void OnUpdate()
        {
            if(!_isHide)
            {
                _isHide = true;
                foreach (GameObject en in Enemies)
                {
                    en.SetActive(false);
                }
            }
            if (ActiveChild() == _squadSize) _isFull = true;
            if (ActiveChild() == 0) _isFull = false;
            if (ActiveChild() < _squadSize && !_isFull)
            {
                FirstSpawn();
                ButtonSpawn();
                StandardSpawn();
            }
        }

        public void Settings()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                var rng = SmartRandom(0, _allTypeEnemies.Count);
                Enemies[i].name = _enemy[rng].Prefab.name + " " + _enemy[rng].Type;
                Enemies[i].transform.GetChild(2).name = Enemies[i].name;
                Enemies[i].transform.SetParent(_spawnPlace);
            }
        }

        public void EnemyOn(int index)
        {

            _offsetSpawn.x = Random.Range(_randomOffsetMinX, _randomOffsetMaxX);
            Enemies[index].transform.position += _offsetSpawn;
            Enemies[index].SetActive(true);
        }

        public int SmartRandom(int min, int max) // ну или не очень смарт, по крайне мере лучше чем обычный
        {
            var rnd = Random.Range(min, max);
            if(rnd == _lastType) _rndStreak++;
            else _rndStreak = 0;
            _lastType = rnd;

            if (_rndStreak == 1)
            {
                for (int i = 0; i < 1; i++)
                {
                    rnd = Random.Range(min, max);
                    if (rnd == _lastType) i--;
                };

            }
            return rnd;
        }

        public int ActiveChild()
        {
            int activeEnemy = 0;
            foreach (Transform child in _spawnPlace)
            {
                if (child.gameObject.activeSelf) activeEnemy++;
            }
            return activeEnemy;
        }

        public void StartAgain()
        {
            _firstDelayTimer = 0;
            _isCanSpawn = false;
        }

        public void SetNewSettings(bool isClickForSpawn, int squadSize, float startDelay, float spawnDelay, float rngOffsetMixX, float rngOffsetMaxX)
        {
            _squadSize = squadSize;
            _isClickForSpawn = isClickForSpawn;
            _startDelayDefault = startDelay;
            _spawnDelayDefault = spawnDelay;
            _randomOffsetMinX = rngOffsetMixX;
            _randomOffsetMaxX = rngOffsetMaxX;
            StartAgain();
        }

        private void FirstSpawn()
        {
            if (_isClickForSpawn) return;
            _firstDelayTimer += Time.deltaTime;
            if (_firstDelayTimer >= _startDelayDefault) _isCanSpawn = true;
        }

        private void ButtonSpawn()
        {
            if (_isClickForSpawn)
            {
                if (Input.GetKeyDown(_settings.SpawnButton)) Spawn();
                return;
            }
        }

        private void StandardSpawn()
        {
            if (_isClickForSpawn) return;
            if (!_isCanSpawn) return;
            _timer += Time.deltaTime;
            if (_timer >= _spawnDelayDefault) Spawn();
        }

        public void Spawn()
        {
            if (_enemyIndex == _poolSize) _enemyIndex = 0;

            _timer = 0;
            EnemyOn(_enemyIndex);
            _enemyIndex++;
        }

        public void TargetSpawn(float hp)
        {
            Spawn();
            Enemies[_enemyIndex - 1].GetComponent<EnemyModel>().SetHP(hp);
        }
    }
}
