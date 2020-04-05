using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatScene
{
    public class Meteor : ProjectileSettings, IUpdate
    {
        public static Meteor Main;

        public int TypeChar;
        private bool _isActive;
        private bool _isHaveMeteor;
        private bool _isDestroyTimer;
        private float _timeToDestroy = 2;
        private float _timer;

        private GameObject _lastMeteor;
        private CharacterSettings _charSet;

        private Vector3 _meteorPos;

        private Dictionary<Vector3, Quaternion> _debrisTransform = new Dictionary<Vector3, Quaternion>(); // ммм осколочки метеорита
        private Dictionary<Vector3, Quaternion> _lastMeteorDebris; // ммм осколочки последнего использумого метеорита
        private List<GameObject> _meteors = new List<GameObject>();

        private RaycastHit hit;
        private Vector3 _dir;

        public void OnUpdate()
        {
            if (_isDestroyTimer) DestroyTimer();

            if (!_isActive)
            {
                Main = this;
                foreach (CharacterModel ch in Object.FindObjectsOfType<CharacterModel>())
                {
                    for (int i = 0; i < ch.Projectiles.Count; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _meteors.Add(ch.Projectiles.ElementAt(i).Value.ElementAt(j));
                        }
                    }
                }
                _charSet = Object.FindObjectOfType<CharacterSettings>();
                _player = Object.FindObjectOfType<CharacterModel>().transform.parent.gameObject;
                _isActive = true;
            }
            if (!_isHaveMeteor) return;
            HitCheck();
            Move();
        }

        public void Starting(int id, float dmg, float speed, Vector3 pos)
        {
            TypeChar = id;
            _damage = dmg;
            _speed = speed;
            CheckMeteor(id, pos);
            ScanEnemy();
            _isHaveMeteor = true;
        }

        public void CheckMeteor(int id, Vector3 pos)
        {
            _timer = 2;
               _maxIndex = _meteors.Count;
            if (_index == _maxIndex) _index = 0;
            _gameObject = _meteors[_index];
            _gameObject.SetActive(true);
            _index++;

            _transform = _gameObject.transform;
            _transform.position = pos;
            _meteorPos = _transform.position;

            _rigibody = _gameObject.GetComponent<Rigidbody>();
            _rigibody.velocity = _transform.forward * _speed;
            SaveDebrisTransform(_transform);
            _debrisTransform.Clear();
        }

        public void SaveDebrisTransform(Transform _transform)
        {
            for (int i = 0; i < _transform.childCount; i++)
            {
                _debrisTransform.Add(_transform.GetChild(i).localPosition, _transform.GetChild(i).localRotation);
            }
            _lastMeteorDebris = _debrisTransform.ToDictionary(entry => entry.Key, entry => entry.Value);
        }
        public void LoadDebrisTransform(Transform _transform)
        {
            _transform.position = _meteorPos;
            for (int i = 0; i < _transform.childCount; i++)
            {
                _transform.GetChild(i).localPosition = _lastMeteorDebris.ElementAt(i).Key;
                _transform.GetChild(i).localRotation = _lastMeteorDebris.ElementAt(i).Value;
            }
            MeshCollidersForeach(true, _transform);
            RigibodyForeach(true, _transform);
            _rigibody.isKinematic = false;
        }

        public void SetSpeed(float speed)
        {
            _speed += speed;
        }

        public void Move()
        {
            if (_isScaleSpeed)
            {
                _isScaleSpeed = false;
            }
            if (_enemies.Count > 0)
            {
                _rigibody.velocity = _transform.forward * _speed;
            }
        }

        public void HitCheck()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_transform.position, 1.5f);
            if (hitColliders.Length < 1) return;

            for (int i = 0; i < hitColliders.Length; i++)
            {
                var enemy = hitColliders[i].GetComponent<EnemyModel>();
                foreach (EnemyModel en in Object.FindObjectsOfType<EnemyModel>())
                {
                    if (en.Enemies.Contains(enemy))
                    {
                        enemy.Damage(_gameObject, _damage);
                    }
                }
                if (hitColliders[i].CompareTag("Ground"))
                {
                    DestroyIt();
                }
            }
        }
        public void RigibodyForeach(bool toggle, Transform target) // для рэгдола 
        {
            foreach (Rigidbody rig in target.GetComponentsInChildren<Rigidbody>())
            {
                rig.isKinematic = toggle;
            }
        }
        public void MeshCollidersForeach(bool toggle, Transform target) // для элементов из нескольких частей
        {
            foreach (MeshCollider col in target.GetComponentsInChildren<MeshCollider>())
            {
                col.isTrigger = toggle;
            }
        }

        public void DestroyIt()
        {
            _timer = 0;
            _isHaveMeteor = false;
            MeshCollidersForeach(false, _transform);
            RigibodyForeach(false, _transform);
            _isDestroyTimer = true;
            _lastMeteor = _gameObject;
        }

        public void DestroyTimer()
        {
            _timer += Time.deltaTime;
            if (_timer > _timeToDestroy)
            {
                _timer = 0;
                _isDestroyTimer = false;
                LoadDebrisTransform(_lastMeteor.transform);
                _lastMeteor.SetActive(false);
            }
        }
    }
}
